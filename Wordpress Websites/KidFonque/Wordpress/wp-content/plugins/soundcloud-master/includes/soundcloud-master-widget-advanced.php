<?php
//Hook Widget
add_action( 'widgets_init', 'soundcloud_master_widget_advanced' );
//Register Widget
function soundcloud_master_widget_advanced() {
register_widget( 'soundcloud_master_widget_advanced' );
}

class soundcloud_master_widget_advanced extends WP_Widget {
	function soundcloud_master_widget_advanced() {
	$widget_ops = array( 'classname' => 'SoundCloud Master Responsive', 'description' => __('SoundCloud Master Advanced Widget is a full featured player that includes all display options. ', 'SoundCloud Master Responsive') );
	$control_ops = array( 'width' => 300, 'height' => 350, 'id_base' => 'soundcloud_master_widget_advanced' );
	$this->WP_Widget( 'soundcloud_master_widget_advanced', __('SoundCloud Master Advanced', 'soundcloud_master'), $widget_ops, $control_ops );
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
		$show_soundcloudplayer = isset( $instance['show_soundcloudplayer'] ) ? $instance['show_soundcloudplayer'] :false;
		$soundcloudplayer_code = $instance['soundcloudplayer_code'];
		$soundcloudplayer_height = $instance['soundcloudplayer_height'];
		$soundcloudplayer_color = $instance['soundcloudplayer_color'];
		$show_soundcloud_autoplay = isset( $instance['show_soundcloud_autoplay'] ) ? $instance['show_soundcloud_autoplay'] :false;
		$show_soundcloud_buying = isset( $instance['show_soundcloud_buying'] ) ? $instance['show_soundcloud_buying'] :false;
		$show_soundcloud_liking = isset( $instance['show_soundcloud_liking'] ) ? $instance['show_soundcloud_liking'] :false;
		$show_soundcloud_download = isset( $instance['show_soundcloud_download'] ) ? $instance['show_soundcloud_download'] :false;
		$show_soundcloud_sharing = isset( $instance['show_soundcloud_sharing'] ) ? $instance['show_soundcloud_sharing'] :false;
		$show_soundcloud_artwork = isset( $instance['show_soundcloud_artwork'] ) ? $instance['show_soundcloud_artwork'] :false;
		$show_soundcloud_comments = isset( $instance['show_soundcloud_comments'] ) ? $instance['show_soundcloud_comments'] :false;
		$show_soundcloud_playcount = isset( $instance['show_soundcloud_playcount'] ) ? $instance['show_soundcloud_playcount'] :false;
		$show_soundcloud_showuser = isset( $instance['show_soundcloud_showuser'] ) ? $instance['show_soundcloud_showuser'] :false;
		$show_soundcloud_bang = isset( $instance['show_soundcloud_bang'] ) ? $instance['show_soundcloud_bang'] :false;
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
	//Prepare Player Height
		if (empty ($soundcloudplayer_height)){
		$soundcloudplayer_height = "450";
		}
	//Prepare Player Color
		if (empty ($soundcloudplayer_color)){
		$soundcloudplayer_color = "ff5500";
		}
	//Prepare Auto-Play
	if ( $show_soundcloud_autoplay ){
		$show_soundcloud_autoplay = "true";
	}
	else{
		$show_soundcloud_autoplay = "false";
	}
	//Prepare UserName
	if ( $show_soundcloud_showuser ){
		$show_soundcloud_showuser = "true";
	}
	else{
		$show_soundcloud_showuser = "false";
	}
	//Prepare Artwork
	if ( $show_soundcloud_artwork ){
		$show_soundcloud_artwork = "true";
	}
	else{
		$show_soundcloud_artwork = "false";
	}
	//Prepare Comments
	if ( $show_soundcloud_comments ){
		$show_soundcloud_comments = "true";
	}
	else{
		$show_soundcloud_comments = "false";
	}
	//Prepare Playcount
	if ( $show_soundcloud_playcount ){
		$show_soundcloud_playcount = "true";
	}
	else{
		$show_soundcloud_playcount = "false";
	}
	//Prepare Share
	if ( $show_soundcloud_sharing ){
		$show_soundcloud_sharing = "true";
	}
	else{
		$show_soundcloud_sharing = "false";
	}
	//Prepare Like
	if ( $show_soundcloud_liking ){
		$show_soundcloud_liking = "true";
	}
	else{
		$show_soundcloud_liking = "false";
	}
	//Prepare Download
	if ( $show_soundcloud_download ){
		$show_soundcloud_download = "true";
	}
	else{
		$show_soundcloud_download = "false";
	}
	//Prepare Buy
	if ( $show_soundcloud_buying ){
		$show_soundcloud_buying = "true";
	}
	else{
		$show_soundcloud_buying = "false";
	}
	//Prepare Bang
	if ( $show_soundcloud_bang ){
		$show_soundcloud_bang = "&amp;hide_related=true&amp;visual=true";
	}
	else{
		$show_soundcloud_bang = false;
	}
	//Display SoundClound Player
	if ( $show_soundcloudplayer ){
		echo '<div>' .
			'<iframe width="100%" height="'.$soundcloudplayer_height.'" scrolling="no" frameborder="no" src="https://w.soundcloud.com/player/?url='.$soundcloudplayer_code.''.$show_soundcloud_bang.'&amp;color='.$soundcloudplayer_color.'&amp;auto_play='.$show_soundcloud_autoplay.'&amp;show_artwork='.$show_soundcloud_artwork.'&amp;show_user='.$show_soundcloud_showuser.'&amp;show_comments='.$show_soundcloud_comments.'&amp;show_playcount='.$show_soundcloud_playcount.'&amp;sharing='.$show_soundcloud_sharing.'&amp;liking='.$show_soundcloud_liking.'&amp;download='.$show_soundcloud_download.'&amp;buying='.$show_soundcloud_buying.'"></iframe>' .
			'</div>';
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
		$instance['show_soundcloudplayer'] = $new_instance['show_soundcloudplayer'];
		$instance['soundcloudplayer_code'] = $new_instance['soundcloudplayer_code'];
		$instance['soundcloudplayer_height'] = $new_instance['soundcloudplayer_height'];
		$instance['soundcloudplayer_color'] = $new_instance['soundcloudplayer_color'];
		$instance['show_soundcloud_autoplay'] = $new_instance['show_soundcloud_autoplay'];
		$instance['show_soundcloud_buying'] = $new_instance['show_soundcloud_buying'];
		$instance['show_soundcloud_liking'] = $new_instance['show_soundcloud_liking'];
		$instance['show_soundcloud_download'] = $new_instance['show_soundcloud_download'];
		$instance['show_soundcloud_sharing'] = $new_instance['show_soundcloud_sharing'];
		$instance['show_soundcloud_artwork'] = $new_instance['show_soundcloud_artwork'];
		$instance['show_soundcloud_comments'] = $new_instance['show_soundcloud_comments'];
		$instance['show_soundcloud_playcount'] = $new_instance['show_soundcloud_playcount'];
		$instance['show_soundcloud_showuser'] = $new_instance['show_soundcloud_showuser'];
		$instance['show_soundcloud_bang'] = $new_instance['show_soundcloud_bang'];
		return $instance;
	}
	function form( $instance ) {
	//Set up some default widget settings.
	$defaults = array( 'soundcloud_title_new' => __('SoundCloud Master', 'soundcloud_master'), 'soundcloud_title' => true, 'soundcloud_title_new' => false, 'show_soundcloudconnect' => false, 'soundcloudconnect_page' => false, 'show_soundcloudlyrics' => false, 'soundcloudlyrics_page' => false, 'show_soundcloudplayer' => false, 'soundcloudplayer_code' => false, 'soundcloudplayer_height' => false, 'soundcloudplayer_color' => false, 'show_soundcloud_autoplay' => false, 'show_soundcloud_buying' => true, 'show_soundcloud_liking' => true, 'show_soundcloud_download' => true, 'show_soundcloud_sharing' => true, 'show_soundcloud_artwork' => true, 'show_soundcloud_comments' => true, 'show_soundcloud_playcount' => true, 'show_soundcloud_showuser' => true, 'show_soundcloud_bang' => false );
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
	<input type="checkbox" <?php checked( (bool) $instance['show_soundcloudplayer'], true ); ?> id="<?php echo $this->get_field_id( 'show_soundcloudplayer' ); ?>" name="<?php echo $this->get_field_name( 'show_soundcloudplayer' ); ?>" />
	<label for="<?php echo $this->get_field_id( 'show_soundcloudplayer' ); ?>"><b><?php _e('SoundCloud Player', 'soundcloud_master'); ?></b></label><br>
	</p>
	<p>
	<label for="<?php echo $this->get_field_id( 'soundcloudplayer_code' ); ?>"><?php _e('SoundClound Track or Set of Tracks Link:', 'soundclound master'); ?></label></br>
	<input id="<?php echo $this->get_field_id( 'soundcloudplayer_code' ); ?>" name="<?php echo $this->get_field_name( 'soundcloudplayer_code' ); ?>" value="<?php echo $instance['soundcloudplayer_code']; ?>" style="width:auto;" />
	</p>
	<div class="description">Copy and Paste from your browser the link from a single track or set of tracks</div>
	<p>
	<label for="<?php echo $this->get_field_id( 'soundcloudplayer_height' ); ?>"><?php _e('Soundcloud Player Height:', 'soundclound master'); ?></label></br>
	<input id="<?php echo $this->get_field_id( 'soundcloudplayer_height' ); ?>" name="<?php echo $this->get_field_name( 'soundcloudplayer_height' ); ?>" value="<?php echo $instance['soundcloudplayer_height']; ?>" style="width:auto;" />
	</p>
	<div class="description">Recommended is 450, you can test different values according to your template</div>
	<p>
	<label for="<?php echo $this->get_field_id( 'soundcloudplayer_color' ); ?>"><?php _e('Soundcloud Player Color:', 'soundclound master'); ?></label></br>
	<input id="<?php echo $this->get_field_id( 'soundcloudplayer_color' ); ?>" name="<?php echo $this->get_field_name( 'soundcloudplayer_color' ); ?>" value="<?php echo $instance['soundcloudplayer_color']; ?>" style="width:auto;" />
	</p>
	<div class="description">Example, <b>ff5500</b> More colors: <a href="http://www.colorpicker.com/" target="_blank">Color Picker</a> Leave blank for default Soundcloud Color.</div>
	<p>
	<img src="<?php echo plugins_url('../images/techgasp-minilogo-16.png', __FILE__); ?>" style="float:left; width:16px; vertical-align:middle;" />
	&nbsp;
	<input type="checkbox" <?php checked( (bool) $instance['show_soundcloud_autoplay'], true ); ?> id="<?php echo $this->get_field_id( 'show_soundcloud_autoplay' ); ?>" name="<?php echo $this->get_field_name( 'show_soundcloud_autoplay' ); ?>" />
	<label for="<?php echo $this->get_field_id( 'show_soundcloud_autoplay' ); ?>"><b><?php _e('Auto-Play', 'soundcloud_master'); ?></b></label><br>
	</p>
	<p>
	<img src="<?php echo plugins_url('../images/techgasp-minilogo-16.png', __FILE__); ?>" style="float:left; width:16px; vertical-align:middle;" />
	&nbsp;
	<input type="checkbox" <?php checked( (bool) $instance['show_soundcloud_showuser'], true ); ?> id="<?php echo $this->get_field_id( 'show_soundcloud_showuser' ); ?>" name="<?php echo $this->get_field_name( 'show_soundcloud_showuser' ); ?>" />
	<label for="<?php echo $this->get_field_id( 'show_soundcloud_showuser' ); ?>"><b><?php _e('Display User', 'soundcloud_master'); ?></b></label><br>
	</p>
	<p>
	<img src="<?php echo plugins_url('../images/techgasp-minilogo-16.png', __FILE__); ?>" style="float:left; width:16px; vertical-align:middle;" />
	&nbsp;
	<input type="checkbox" <?php checked( (bool) $instance['show_soundcloud_artwork'], true ); ?> id="<?php echo $this->get_field_id( 'show_soundcloud_artwork' ); ?>" name="<?php echo $this->get_field_name( 'show_soundcloud_artwork' ); ?>" />
	<label for="<?php echo $this->get_field_id( 'show_soundcloud_artwork' ); ?>"><b><?php _e('Display Artwork', 'soundcloud_master'); ?></b></label><br>
	</p>
	<p>
	<img src="<?php echo plugins_url('../images/techgasp-minilogo-16.png', __FILE__); ?>" style="float:left; width:16px; vertical-align:middle;" />
	&nbsp;
	<input type="checkbox" <?php checked( (bool) $instance['show_soundcloud_comments'], true ); ?> id="<?php echo $this->get_field_id( 'show_soundcloud_comments' ); ?>" name="<?php echo $this->get_field_name( 'show_soundcloud_comments' ); ?>" />
	<label for="<?php echo $this->get_field_id( 'show_soundcloud_comments' ); ?>"><b><?php _e('Display Comments', 'soundcloud_master'); ?></b></label><br>
	</p>
	<p>
	<img src="<?php echo plugins_url('../images/techgasp-minilogo-16.png', __FILE__); ?>" style="float:left; width:16px; vertical-align:middle;" />
	&nbsp;
	<input type="checkbox" <?php checked( (bool) $instance['show_soundcloud_playcount'], true ); ?> id="<?php echo $this->get_field_id( 'show_soundcloud_playcount' ); ?>" name="<?php echo $this->get_field_name( 'show_soundcloud_playcount' ); ?>" />
	<label for="<?php echo $this->get_field_id( 'show_soundcloud_playcount' ); ?>"><b><?php _e('Display Playcount', 'soundcloud_master'); ?></b></label><br>
	</p>
	<p>
	<img src="<?php echo plugins_url('../images/techgasp-minilogo-16.png', __FILE__); ?>" style="float:left; width:16px; vertical-align:middle;" />
	&nbsp;
	<input type="checkbox" <?php checked( (bool) $instance['show_soundcloud_sharing'], true ); ?> id="<?php echo $this->get_field_id( 'show_soundcloud_sharing' ); ?>" name="<?php echo $this->get_field_name( 'show_soundcloud_sharing' ); ?>" />
	<label for="<?php echo $this->get_field_id( 'show_soundcloud_sharing' ); ?>"><b><?php _e('Display Share', 'soundcloud_master'); ?></b></label><br>
	</p>
	<p>
	<img src="<?php echo plugins_url('../images/techgasp-minilogo-16.png', __FILE__); ?>" style="float:left; width:16px; vertical-align:middle;" />
	&nbsp;
	<input type="checkbox" <?php checked( (bool) $instance['show_soundcloud_liking'], true ); ?> id="<?php echo $this->get_field_id( 'show_soundcloud_liking' ); ?>" name="<?php echo $this->get_field_name( 'show_soundcloud_liking' ); ?>" />
	<label for="<?php echo $this->get_field_id( 'show_soundcloud_liking' ); ?>"><b><?php _e('Display Like', 'soundcloud_master'); ?></b></label><br>
	</p>
	<p>
	<img src="<?php echo plugins_url('../images/techgasp-minilogo-16.png', __FILE__); ?>" style="float:left; width:16px; vertical-align:middle;" />
	&nbsp;
	<input type="checkbox" <?php checked( (bool) $instance['show_soundcloud_download'], true ); ?> id="<?php echo $this->get_field_id( 'show_soundcloud_download' ); ?>" name="<?php echo $this->get_field_name( 'show_soundcloud_download' ); ?>" />
	<label for="<?php echo $this->get_field_id( 'show_soundcloud_download' ); ?>"><b><?php _e('Display Download', 'soundcloud_master'); ?></b></label><br>
	</p>
	<p>
	<img src="<?php echo plugins_url('../images/techgasp-minilogo-16.png', __FILE__); ?>" style="float:left; width:16px; vertical-align:middle;" />
	&nbsp;
	<input type="checkbox" <?php checked( (bool) $instance['show_soundcloud_buying'], true ); ?> id="<?php echo $this->get_field_id( 'show_soundcloud_buying' ); ?>" name="<?php echo $this->get_field_name( 'show_soundcloud_buying' ); ?>" />
	<label for="<?php echo $this->get_field_id( 'show_soundcloud_buying' ); ?>"><b><?php _e('Display Buy', 'soundcloud_master'); ?></b></label><br>
	</p>
	<br>
	<p>
	<img src="<?php echo plugins_url('../images/techgasp-minilogo-16.png', __FILE__); ?>" style="float:left; width:16px; vertical-align:middle;" />
	&nbsp;
	<input type="checkbox" <?php checked( (bool) $instance['show_soundcloud_bang'], true ); ?> id="<?php echo $this->get_field_id( 'show_soundcloud_bang' ); ?>" name="<?php echo $this->get_field_name( 'show_soundcloud_bang' ); ?>" />
	<label for="<?php echo $this->get_field_id( 'show_soundcloud_bang' ); ?>"><b><?php _e('Activate TechGasp BANG! Artwork', 'soundcloud_master'); ?></b></label><br>
	</p>
	<p>
	<div class="description">Activate to test, for users with great Cover Artworks. Some of the above player options will be disabled.</div>
	</P>
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