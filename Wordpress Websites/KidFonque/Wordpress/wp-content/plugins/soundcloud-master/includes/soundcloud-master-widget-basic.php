<?php
//Hook Widget
add_action( 'widgets_init', 'soundcloud_master_widget_basic' );
//Register Widget
function soundcloud_master_widget_basic() {
register_widget( 'soundcloud_master_widget_basic' );
}

class soundcloud_master_widget_basic extends WP_Widget {
	function soundcloud_master_widget_basic() {
	$widget_ops = array( 'classname' => 'SoundCloud Master Basic', 'description' => __('SoundCloud Master Basic Widget is a fast loading player, excellent for Single tracks. ', 'SoundCloud Master Basic') );
	$control_ops = array( 'width' => 300, 'height' => 350, 'id_base' => 'soundcloud_master_widget_basic' );
	$this->WP_Widget( 'soundcloud_master_widget_basic', __('SoundCloud Master Basic', 'soundcloud_master'), $widget_ops, $control_ops );
	}

	function widget( $args, $instance ) {
		extract( $args );
		//Our variables from the widget settings.
		$soundcloud_title = isset( $instance['soundcloud_title'] ) ? $instance['soundcloud_title'] :false;
		$soundcloud_title_new = isset( $instance['soundcloud_title_new'] ) ? $instance['soundcloud_title_new'] :false;
		$soundcloudspacer ="'";
		$show_soundcloudplayer = isset( $instance['show_soundcloudplayer'] ) ? $instance['show_soundcloudplayer'] :false;
		$soundcloudplayer_code = $instance['soundcloudplayer_code'];
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
	//Display SoundClound Player
	if ( $show_soundcloudplayer ){
		echo '<div>' .
			'<iframe width="100%" height="100%" scrolling="no" frameborder="no" src="https://w.soundcloud.com/player/?url='.$soundcloudplayer_code.'&amp;color=ff5500&amp;auto_play=false&amp;show_artwork=true&amp;show_user=true&amp;show_comments=true&amp;show_playcount=true&amp;sharing=true&amp;liking=true&amp;download=true&amp;buying=true"></iframe>' .
			'</div>';
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
		$instance['show_soundcloudplayer'] = $new_instance['show_soundcloudplayer'];
		$instance['soundcloudplayer_code'] = $new_instance['soundcloudplayer_code'];
		return $instance;
	}
	function form( $instance ) {
	//Set up some default widget settings.
	$defaults = array( 'soundcloud_title_new' => __('SoundCloud Master', 'soundcloud_master'), 'soundcloud_title' => true, 'soundcloud_title_new' => false, 'show_soundcloudplayer' => false, 'soundcloudplayer_code' => false );
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
	<input type="checkbox" <?php checked( (bool) $instance['show_soundcloudplayer'], true ); ?> id="<?php echo $this->get_field_id( 'show_soundcloudplayer' ); ?>" name="<?php echo $this->get_field_name( 'show_soundcloudplayer' ); ?>" />
	<label for="<?php echo $this->get_field_id( 'show_soundcloudplayer' ); ?>"><b><?php _e('SoundCloud Player', 'soundcloud_master'); ?></b></label><br>
	</p>
	<p>
	<label for="<?php echo $this->get_field_id( 'soundcloudplayer_code' ); ?>"><?php _e('SoundClound Track or Set of Tracks Link:', 'soundclound master'); ?></label></br>
	<input id="<?php echo $this->get_field_id( 'soundcloudplayer_code' ); ?>" name="<?php echo $this->get_field_name( 'soundcloudplayer_code' ); ?>" value="<?php echo $instance['soundcloudplayer_code']; ?>" style="width:auto;" />
	</p>
	<div class="description">Copy and Paste from your browser the link from a single track or set of tracks</div>
	</br>
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