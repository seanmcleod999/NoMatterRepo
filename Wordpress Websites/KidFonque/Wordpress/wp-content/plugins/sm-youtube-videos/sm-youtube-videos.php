<?php
/*
Plugin Name: SM Youtube Videos
Description: A simple youtube videos pluign.
Version: 0.1
Author: Sean Mcleod
Plugin URI: http://www.redorange.co.za/sm-youtube-videos
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

// REGISTER CUSTOM POST TYPE

function sm_youtube_videos() {
  $labels = array(
    'name'               => _x( 'Videos', 'post type general name' ),
    'singular_name'      => _x( 'Video', 'post type singular name' ),
    'add_new'            => _x( 'Add New', 'book' ),
    'add_new_item'       => __( 'Add New Video' ),
    'edit_item'          => __( 'Edit Video' ),
    'new_item'           => __( 'New Video' ),
    'all_items'          => __( 'All Videos' ),
    'view_item'          => __( 'View Video' ),
    'search_items'       => __( 'Search Videos' ),
    'not_found'          => __( 'No Videos found' ),
    'not_found_in_trash' => __( 'No Video found in the Trash' ), 
    'parent_item_colon'  => '',
    'menu_name'          => 'Videos'
  );
  $args = array(
    'labels'        => $labels,
    'hierarchical' => true,
    'description'   => 'Holds your youtube videos',
    'public'        => true,
    'menu_position' => 6,
    'supports'      => array( 'title' ),
    'has_archive'   => true,
  );
  register_post_type( 'video', $args ); 
}
add_action( 'init', 'sm_youtube_videos' );

// END REGISTER CUSTOM POST TYPE

// REGISTER STYLESHEET

function sm_youtube_videos_load_stylesheet() {
    $url = plugins_url('/css/sm-youtube-videos.css', __FILE__);
    wp_register_style('sm_youtube_videos_css', $url);
    wp_enqueue_style( 'sm_youtube_videos_css');
}
add_action('wp_print_styles', 'sm_youtube_videos_load_stylesheet');

//END REGISTER STYLESHEET


//SHORT CODE FUNCTIONS

function sm_youtube_vidoes_func( $atts ){
	return sm_youtube_vidoes_getrows($atts);
}
add_shortcode( 'sm_youtube_vidoes', 'sm_youtube_vidoes_func' );

function sm_youtube_vidoes_getrows($atts) {
   
    $output = '';
    $videos_query = new WP_Query('post_type=video&showposts=1');

    if ($videos_query->have_posts()) :
    $output .= '<div class="YouTubeVideosBlock clearfix">';
    
    while ($videos_query->have_posts()) : $videos_query->the_post();
        $output .= '<h2>YouTube - ' . get_the_title() . '</h2>';


        $output .= do_shortcode('[fve]'. get_post_meta(get_the_id(), 'video_url', $single = true) .'[/fve]');

    endwhile;

     $output .= "<div id=\"ViewAllVideosLink\"><a href=" . get_site_url() . "/video class=\"button red\">view all videos</a></div>";
    
    $output .= '</div>';
    endif;
    wp_reset_postdata(); 

    return $output;
}

//END SHORT CODE FUNCTIONS

?>