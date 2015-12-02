<?php
/*
Plugin Name: SM Album History
Description: A simple Album History for musicians.
Version: 0.1
Author: Sean Mcleod
Plugin URI: http://www.redorange.co.za/sm-discography
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

function sm_album_history() {
  $labels = array(
    'name'               => _x( 'Albums', 'post type general name' ),
    'singular_name'      => _x( 'Album', 'post type singular name' ),
    'add_new'            => _x( 'Add New', 'book' ),
    'add_new_item'       => __( 'Add New Album' ),
    'edit_item'          => __( 'Edit Album' ),
    'new_item'           => __( 'New Album' ),
    'all_items'          => __( 'All Albums' ),
    'view_item'          => __( 'View Album' ),
    'search_items'       => __( 'Search Albums' ),
    'not_found'          => __( 'No albums found' ),
    'not_found_in_trash' => __( 'No albums found in the Trash' ), 
    'parent_item_colon'  => '',
    'menu_name'          => 'Albums'
  );
  $args = array(
    'labels'        => $labels,
    'description'   => 'Holds your albums and album specific data',
    'public'        => true,
    'menu_position' => 5,
    'supports'      => array( 'title', 'editor', 'thumbnail', 'custom-fields' ),
    'has_archive'   => true,
  );
  register_post_type( 'album', $args ); 
}
add_action( 'init', 'sm_album_history' );

//END REGISTER CUSTOM POST TYPE

//CUSTOM POST TYPE LIST COLUMNS

add_filter('manage_album_posts_columns', 'bs_album_table_head');
function bs_album_table_head( $defaults ) {
    $defaults['date_released']  = 'Date Released';
    return $defaults;
}

add_action( 'manage_album_posts_custom_column', 'bs_album_table_content', 10, 2 );

function bs_album_table_content( $column_name, $post_id ) {
    if ($column_name == 'date_released') {
    $event_date = get_post_meta( $post_id, 'date_released', true );
      echo  date( _x( 'F d, Y', 'Album date format', 'textdomain' ), strtotime( $event_date ) );
    }
}

//END CUSTOM POST TYPE LIST COLUMNS

// REGISTER STYLESHEET

function sm_album_history_load_stylesheet() {
    $url = plugins_url('/css/sm-album-history.css', __FILE__);
    wp_register_style('sm_album_history_css', $url);
    wp_enqueue_style( 'sm_album_history_css');
}
add_action('wp_print_styles', 'sm_album_history_load_stylesheet');

//END REGISTER STYLESHEET

// WIDGET CODE

// Creating the widget 
class sm_album_history_latest_album_widget extends WP_Widget {

function __construct() {
    parent::__construct(
    // Base ID of your widget
    'sm_album_history_latest_album_widget', 

    // Widget name will appear in UI
    __('Latest Album Widget', 'sm_album_history_latest_album_widget_domain'), 

    // Widget description
    array( 'description' => __( 'Will display the latest album posted', 'sm_album_history_latest_album_widget_domain' ), ) 
    );
}


// Creating widget front-end
// This is where the action happens
public function widget( $args, $instance ) {
    $title = apply_filters( 'widget_title', $instance['title'] );
    // before and after widget arguments are defined by themes
    echo $args['before_widget'];
    if ( ! empty( $title ) )
    echo $args['before_title'] . $title . $args['after_title'];

    // This is where you run the code and display the output
    //echo __( 'Hello, World!', 'sm_album_history_latest_album_widget_domain' );


     $args2 = array(
        'post_type' => 'album',
        'posts_per_page' => '1',
        'orderby' => 'meta_value',
        'meta_key' => 'date_released',
        'order' => 'DESC'
     );

    $album_query = new WP_Query($args2);

     if ($album_query->have_posts()) :
      
         while ($album_query->have_posts()) : $album_query->the_post();
       
            echo "<span class='albumtitle'>" . get_the_title() . "</span>";

              $imageid = get_post_meta(get_the_ID(), 'album_cover', $single = true); 
              echo "<a href=" . get_permalink(get_the_ID()) . ">";
              echo wp_get_attachment_image($imageid, 'medium');  
              echo "</a>";
        
         endwhile;

     endif;

     wp_reset_postdata(); 

    echo $args['after_widget'];
}
		
// Widget Backend 
public function form( $instance ) {
    if ( isset( $instance[ 'title' ] ) ) {
    $title = $instance[ 'title' ];
    }
    else {
    $title = __( 'New title', 'sm_album_history_latest_album_widget_domain' );
}
// Widget admin form
?>
<p>
<label for="<?php echo $this->get_field_id( 'title' ); ?>"><?php _e( 'Title:' ); ?></label> 
<input class="widefat" id="<?php echo $this->get_field_id( 'title' ); ?>" name="<?php echo $this->get_field_name( 'title' ); ?>" type="text" value="<?php echo esc_attr( $title ); ?>" />
</p>
<?php 
}
	
// Updating widget replacing old instances with new
public function update( $new_instance, $old_instance ) {
    $instance = array();
    $instance['title'] = ( ! empty( $new_instance['title'] ) ) ? strip_tags( $new_instance['title'] ) : '';
    return $instance;
    }
} // Class wpb_widget ends here

// Register and load the widget
function sm_album_history_latest_album_widget() {
	register_widget( 'sm_album_history_latest_album_widget' );
}
add_action( 'widgets_init', 'sm_album_history_latest_album_widget' );

// END WIDGET CODE

?>