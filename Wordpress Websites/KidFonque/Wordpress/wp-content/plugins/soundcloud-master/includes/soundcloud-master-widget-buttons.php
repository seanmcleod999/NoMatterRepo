<?php
//Hook Widget
add_action( 'widgets_init', 'soundcloud_master_widget_buttons' );
//Register Widget
function soundcloud_master_widget_buttons() {
register_widget( 'soundcloud_master_widget_buttons' );
}

class soundcloud_master_widget_buttons extends WP_Widget {
	function soundcloud_master_widget_buttons() {
	$widget_ops = array( 'classname' => 'SoundCloud Master Buttons', 'description' => __('SoundCloud Master Buttons Widget allows you to display the SoundCloud Connect and Lyrics Button. ', 'SoundCloud Master Buttons') );
	$control_ops = array( 'width' => 300, 'height' => 350, 'id_base' => 'soundcloud_master_widget_buttons' );
	$this->WP_Widget( 'soundcloud_master_widget_buttons', __('SoundCloud Master Buttons', 'soundcloud_master'), $widget_ops, $control_ops );
	}
	
	function widget( $args, $instance ) {
		extract( $args );
		//Our variables from the widget settings.
		$soundcloud_title = isset( $instance['soundcloud_title'] ) ? $instance['soundcloud_title'] :false;
		$soundcloud_title_new = isset( $instance['soundcloud_title_new'] ) ? $instance['soundcloud_title_new'] :false;
		$soundcloudspacer ="'";
		$show_soundcloudconnect = isset( $instance['show_soundcloudconnect'] ) ? $instance['show_soundcloudconnect'] :false;
		$soundcloudconnect_page = $instance['soundcloudconnect_page'];
		$show_soundcloudlyrics = isset( $instance['show_soundcloudlyrics'] ) ? $instance['show_soundcloudlyrics'] :false;
		$soundcloudlyrics_page = $instance['soundcloudlyrics_page'];
		echo $before_widget;
		
		// Display the widget title
	if ( $soundcloud_title ){
		if (empty ($soundcloud_title_new)){
		$soundcloud_title_new = "Soundcloud Master";
		}
		echo $before_title . $soundcloud_title_new . $after_title;
	}
	else{
	}
	//Prepare Connect Button
		if (empty ($soundcloudconnect_page)){
		$soundcloudconnect_page = "https://soundcloud.com/";
		}
	//Display SoundClound Connect Button
	if ( $show_soundcloudconnect ){
			$url_loc = plugins_url();
			echo '<a href="'.$soundcloudconnect_page.'" target="_blank"><img src="'.$url_loc.'/soundcloud-master/images/btn-connect-s.png"></a>' .
					'&nbsp;';
	}
	else{
	}
	//Prepare Lyrics Button
		if (empty ($soundcloudlyrics_page)){
		$soundcloudlyrics_page = "https://soundcloud.com/";
		}
	//Display SoundCloud Lyrics Button
	if ( $show_soundcloudlyrics ){
			$url_loc = plugins_url();
			echo '<a href="'.$soundcloudlyrics_page.'" target="_blank"><img src="'.$url_loc.'/soundcloud-master/images/btn-lyrics.png"></a>';
	}
	else{
	}
	echo $after_widget;
	}
	//Update the widget
	function update( $new_instance, $old_instance ) {
		$instance = $old_instance;
		//Strip tags from title and name to remove HTML
		$instance['soundcloud_title'] = strip_tags( $new_instance['soundcloud_title'] );
		$instance['soundcloud_title_new'] = $new_instance['soundcloud_title_new'];
		$instance['show_soundcloudconnect'] = $new_instance['show_soundcloudconnect'];
		$instance['soundcloudconnect_page'] = $new_instance['soundcloudconnect_page'];
		$instance['show_soundcloudlyrics'] = $new_instance['show_soundcloudlyrics'];
		$instance['soundcloudlyrics_page'] = $new_instance['soundcloudlyrics_page'];
		return $instance;
	}
	function form( $instance ) {
	//Set up some default widget settings.
	$defaults = array( 'soundcloud_title_new' => __('SoundCloud Master', 'soundcloud_master'), 'soundcloud_title' => true, 'soundcloud_title_new' => false, 'show_soundcloudconnect' => false, 'soundcloudconnect_page' => false, 'show_soundcloudlyrics' => false, 'soundcloudlyrics_page' => false );
	$instance = wp_parse_args( (array) $instance, $defaults );
	?>
		<br>
		<b>Check the buttons to be displayed:</b>
	<p>
	<img src="<?php echo plugins_url('../images/techgasp-minilogo-16.png', __FILE__); ?>" style="float:left; height:16px; vertical-align:middle;" />
	&nbsp;
	<input type="checkbox" <?php checked( (bool) $instance['soundcloud_title'], true ); ?> id="<?php echo $this->get_field_id( 'soundcloud_title' ); ?>" name="<?php echo $this->get_field_name( 'soundcloud_title' ); ?>" />
	<label for="<?php echo $this->get_field_id( 'soundcloud_title' ); ?>"><b><?php _e('Display Widget Title', 'soundcloud_master'); ?></b></label></br>
	</p>
	<p>
	<label for="<?php echo $this->get_field_id( 'soundcloud_title_new' ); ?>"><?php _e('Change Title:', 'soundcloud_master'); ?></label>
	<br>
	<input id="<?php echo $this->get_field_id( 'soundcloud_title_new' ); ?>" name="<?php echo $this->get_field_name( 'soundcloud_title_new' ); ?>" value="<?php echo $instance['soundcloud_title_new']; ?>" style="width:auto;" />
	</p>
<div style="background: url(<?php echo plugins_url('../images/techgasp-hr.png', __FILE__); ?>) repeat-x; height: 10px"></div>
	<p>
	<img src="<?php echo plugins_url('../images/techgasp-minilogo-16.png', __FILE__); ?>" style="float:left; width:16px; vertical-align:middle;" />
	&nbsp;
	<input type="checkbox" <?php checked( (bool) $instance['show_soundcloudconnect'], true ); ?> id="<?php echo $this->get_field_id( 'show_soundcloudconnect' ); ?>" name="<?php echo $this->get_field_name( 'show_soundcloudconnect' ); ?>" />
	<label for="<?php echo $this->get_field_id( 'show_soundcloudconnect' ); ?>"><b><?php _e('SoundCloud Connect Button', 'soundcloud_master'); ?></b></label></br>
	</p>
	<p>
	<label for="<?php echo $this->get_field_id( 'soundcloudconnect_page' ); ?>"><?php _e('insert SoundCloud User Page link:', 'soundcloud_master'); ?></label>
	<input id="<?php echo $this->get_field_id( 'soundcloudconnect_page' ); ?>" name="<?php echo $this->get_field_name( 'soundcloudconnect_page' ); ?>" value="<?php echo $instance['soundcloudconnect_page']; ?>" style="width:auto;" />
	</p>
<div style="background: url(<?php echo plugins_url('../images/techgasp-hr.png', __FILE__); ?>) repeat-x; height: 10px"></div>
	<p>
	<img src="<?php echo plugins_url('../images/techgasp-minilogo-16.png', __FILE__); ?>" style="float:left; width:16px; vertical-align:middle;" />
	&nbsp;
	<input type="checkbox" <?php checked( (bool) $instance['show_soundcloudlyrics'], true ); ?> id="<?php echo $this->get_field_id( 'show_soundcloudlyrics' ); ?>" name="<?php echo $this->get_field_name( 'show_soundcloudlyrics' ); ?>" />
	<label for="<?php echo $this->get_field_id( 'show_soundcloudlyrics' ); ?>"><b><?php _e('Soundcloud Lyrics Button', 'soundcloud_master'); ?></b></label></br>
	</p>
	<p>
	<label for="<?php echo $this->get_field_id( 'soundcloudlyrics_page' ); ?>"><?php _e('insert SoundClound Lyrics Link:', 'soundclound master'); ?></label></br>
	<input id="<?php echo $this->get_field_id( 'soundcloudlyrics_page' ); ?>" name="<?php echo $this->get_field_name( 'soundcloudlyrics_page' ); ?>" value="<?php echo $instance['soundcloudlyrics_page']; ?>" style="width:auto;" />
	</p>
<div style="background: url(<?php echo plugins_url('../images/techgasp-hr.png', __FILE__); ?>) repeat-x; height: 10px"></div>
		<p>
		<img src="<?php echo plugins_url('../images/techgasp-minilogo-16.png', __FILE__); ?>" style="float:left; width:16px; vertical-align:middle;" />
		&nbsp;
		<b><?php echo get_option('soundcloud_master_name'); ?> Website</b>
		</p>
		<p><a class="button-secondary" href="http://wordpress.techgasp.com/soundcloud-master/" target="_blank" title="<?php echo get_option('soundcloud_master_name'); ?> Info Page">Info Page</a> <a class="button-secondary" href="http://wordpress.techgasp.com/soundcloud-master-documentation/" target="_blank" title="<?php echo get_option('soundcloud_master_name'); ?> Documentation">Documentation</a> <a class="button-primary" href="http://wordpress.org/plugins/soundcloud-master/" target="_blank" title="<?php echo get_option('soundcloud_master_name'); ?> Wordpress">RATE US *****</a></p>
	<?php
	}
 }
?>