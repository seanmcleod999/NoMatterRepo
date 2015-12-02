<?php
/*
Plugin Name: SM Event Calendar
Description: A simple event calendar
Version: 0.1
Author: Sean Mcleod
Plugin URI: http://www.redorange.co.za/sm-event-calendar
Author URI: http://www.redorange.co.za


Copyright (C) 2014 Sean Mcleod (email: mcleod.sean@gmail.com)

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

The program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

*/

define('SM_EVENT_CALENDAR_ALT_API', 'http://www.redorange.co.za/sm-event-calendar-api/');

//hook into the plugins update check
add_filter('pre_set_site_transient_update_plugins','sm_event_calendar_check');

//Check alternative API before transient is saved
function sm_event_calendar_check( $transient ) {
    //Check if the transient contains the 'checked' information
    //If no, just return its value without checking it
    if ( empty( $transient->checked) ) return $transient;

    //the transient contains the 'checked' information
    //Now append to it inforamtion from your own API

    $plugin_slug = plugin_basename(__FILE__);

    //POST data to send to your API

    $args = array('action' => 'update-check',
    'plugin_name' => $plugin_slug,
    'version' => $transient->checked[$plugin_slug]);

    //Send request checking for an update
    $response = sm_event_calendar_request( $args );

    //If response is false, dont alter the transient
    if( FALSE != $response) {
        $transient->response[$plugin_slug] = $response;
    }

    return $transient;
}


//Send a request to the alternative API, return an object or false
function sm_event_calendar_request( $args )
{
    //Send request
    $request = wp_remote_post(SM_EVENT_CALENDAR_ALT_API, array('body' => $args));

    //Make sure the request was successful
    if( is_wp_error( $request ) or wp_remote_retrieve_response_code( $request ) != 200 )
    {
        //request failed
        return FALSE;
    }

    //Read server response, which should be an object
    $response = unserialize(wp_remote_retrieve_body( $request ));

    if (is_object( $response))
    {
        return $response;
    } else {
        //Unexpected response
        return FALSE;
    }

}

//Hook into the plugin details screen
add_filter('plugins_api', 'sm_event_calendar_information', 10, 3);

function sm_event_calendar_information($false, $action, $args)
{
    $plugin_slug = plugin_basename(__FILE__);

    //check if the plugins API is about this plugin
    if($args->slug != $plugin_slug) {
        return FALSE;
    }

    //POST data to send to your API
    $args = array(
    'action' => 'plugin_information',
    'plugin_name' => $plugin_slug,
    'version_compare' => $transient->checked[$plugin_slug]
    );

    //Send request for detailed information
    $response = sm_event_calendar_request( $args );

    $request = wp_remote_post(SM_EVENT_CALENDAR_ALT_API, array( 'body' => $args ));

    return $response;
}

// REGISTER CUSTOM POST TYPE

function sm_event_calendar() {
  $labels = array(
    'name'               => _x( 'Gigs', 'post type general name' ),
    'singular_name'      => _x( 'Gig', 'post type singular name' ),
    'add_new'            => _x( 'Add Gig', 'book' ),
    'add_new_item'       => __( 'Add New Gig' ),
    'edit_item'          => __( 'Edit Gig' ),
    'new_item'           => __( 'New Gig' ),
    'all_items'          => __( 'All Gigs' ),
    'view_item'          => __( 'View Gig' ),
    'search_items'       => __( 'Search Gigs' ),
    'not_found'          => __( 'No Gigs found' ),
    'not_found_in_trash' => __( 'No Gigs found in the Trash' ), 
    'parent_item_colon'  => '',
    'menu_name'          => 'Gigs'
  );
  $args = array(
    'labels'        => $labels,
    'description'   => 'Holds your Gigs',
    'public'        => true,
    'menu_position' => 7,
    'supports'      => array( 'title' ),
    'has_archive'   => true,
  );
  register_post_type( 'gigs', $args ); 
}
add_action( 'init', 'sm_event_calendar' );

//END REGISTER CUSTOM POST TYPE

//CUSTOM POST TYPE LIST COLUMNS

add_filter('manage_gigs_posts_columns', 'bs_gigs_table_head');
function bs_gigs_table_head( $defaults ) {
    $defaults['gig_date']  = 'Gig Date';
    $defaults['gig_venue']    = 'Gig Venue';
    return $defaults;
}

add_action( 'manage_gigs_posts_custom_column', 'bs_gigs_table_content', 10, 2 );

function bs_gigs_table_content( $column_name, $post_id ) {
    if ($column_name == 'gig_date') {
    $event_date = get_post_meta( $post_id, 'gig_date', true );
      echo  date( _x( 'F d, Y', 'Event date format', 'textdomain' ), strtotime( $event_date ) );
    }
    if ($column_name == 'gig_venue') {
    $status = get_post_meta( $post_id, 'gig_venue', true );
    echo $status;
    }
}

//END CUSTOM POST TYPE LIST COLUMNS

// REGISTER STYLESHEET

function sm_event_calendar_load_stylesheet() {
    $url = plugins_url('/css/sm-event-calendar.css', __FILE__);
    wp_register_style('sm_event_calendar_css', $url);
    wp_enqueue_style( 'sm_event_calendar_css');
}
add_action('wp_print_styles', 'sm_event_calendar_load_stylesheet');

//END REGISTER STYLESHEET

//SHORT CODE FUNCTIONS

function sm_event_calendar_func( $atts ){
	return sm_event_calendar_getrows($atts);
}
add_shortcode( 'sm_event_calendar', 'sm_event_calendar_func' );

function sm_event_calendar_getrows($atts) {
   
    $output = '';

    $today = date('Ymd');
    $args = array(
        'post_type' => 'gigs',
        'posts_per_page' => '20',
        'meta_key' => 'gig_date',
        'meta_query' => array(
            array(
                'key' => 'gig_date',
                'value' => $today,
                'compare' => '>='
            )
        ),
        'orderby' => 'meta_value',
                    'meta_key' => 'gig_date',
        'order' => 'ASC'
    );

    $events_query = new WP_Query($args);

    if ($events_query->have_posts()) :
    $output .= '<div class="GigsBlock clearfix">';
    
    $output .= "<h2 id=\"sm-event-cal-list-title\">Upcoming Gigs</h2>";

     $output .= "<ul id=\"sm-event-cal-list\">";
    
    while ($events_query->have_posts()) : $events_query->the_post();
       
        $output .= "<li>";

        $output .= "<a href=" . get_permalink(get_the_ID()) . ">";

        $output .= "<div id=\"sm-event-cal-list-gigdetails\">";

        $output .= "<span class=\"gigdate\">";
        $output .= mysql2date('l j F', get_post_meta(get_the_id(), 'gig_date', $single = true));
        $output .= "</span>\n";

	    $output .= "<span class=\"title\">";
        $output .= get_the_title() . " @ " . get_post_meta(get_the_id(), 'gig_venue', $single = true);
        $output .= "</span>\n";


        //$output .= "<span class=\"venue\">";
        //$output .= get_post_meta(get_the_id(), 'gig_venue', $single = true);
        //$output .= "</span>\n";



        //$output .= "<span class=\"moredetails\">";
        //$output .= "<a href=" . get_permalink(get_the_ID()) . " class=\"button red\">more details</a>";
        //$output .= "</span>\n";

        $output .= "<div class=\"clear\"><!-- --></div>";

         $output .= "</div>";

        $output .= "</a>";

        $output .= "</li>";
        
    endwhile;

    $output .= "</ul>";

    $output .= "<div id=\"ViewAllGigsLink\"><a href=" . get_site_url() . "/gigs class=\"button red\">view all gigs</a></div>";
    
    $output .= '</div>';
    endif;
    wp_reset_postdata(); 

    return $output;
}

//END SHORT CODE FUNCTIONS



?>