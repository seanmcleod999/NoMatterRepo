<?php
/*******************************************************************
* Adds a box to the main column on the Post and Page edit screens.*
*******************************************************************/
function soundcloud_master_add_custom_box() {
$screens = array( 'post', 'page' );

foreach ( $screens as $screen ) {

	add_meta_box(
		'soundcloud_master_sectionid',
		__( 'Soundcloud Master Shortcode [soundcloud-master]', 'soundcloud_master_shortcode' ),
		'soundcloud_master_inner_custom_box',
		$screen
	);
}
}
add_action( 'add_meta_boxes', 'soundcloud_master_add_custom_box' );

/**
* Prints the box content.
*
* @param WP_Post $post The object for the current post/page.
*/
function soundcloud_master_inner_custom_box( $post ) {

// Add an nonce field so we can check for it later.
	wp_nonce_field( 'soundcloud_master_inner_custom_box', 'soundcloud_master_inner_custom_box_nonce' );

/*
* Use get_post_meta() to retrieve an existing value
* from the database and use the value for the form.
*/
	$soundcloud_master_preview = get_post_meta( $post->ID, 'soundcloud_master_preview', true );
	$soundcloud_master_align = get_post_meta( $post->ID, 'soundcloud_master_align', true );
	$soundcloud_master_button_connect = get_post_meta( $post->ID, 'soundcloud_master_button_connect', true );
	$soundcloud_master_button_page_connect = get_post_meta( $post->ID, 'soundcloud_master_button_page_connect', true );
	$soundcloud_master_button_lyrics = get_post_meta( $post->ID, 'soundcloud_master_button_lyrics', true );
	$soundcloud_master_button_page_lyrics = get_post_meta( $post->ID, 'soundcloud_master_button_page_lyrics', true );
	$soundcloud_master_player = get_post_meta( $post->ID, 'soundcloud_master_player', true );
	$soundcloud_master_player_page = get_post_meta( $post->ID, 'soundcloud_master_player_page', true );
	$soundcloud_master_player_width = get_post_meta( $post->ID, 'soundcloud_master_player_width', true );
	$soundcloud_master_player_height = get_post_meta( $post->ID, 'soundcloud_master_player_height', true );
	$soundcloud_master_player_color = get_post_meta( $post->ID, 'soundcloud_master_player_color', true );
	$soundcloud_master_player_autoplay = get_post_meta( $post->ID, 'soundcloud_master_player_autoplay', true );
	$soundcloud_master_player_buying = get_post_meta( $post->ID, 'soundcloud_master_player_buying', true );
	$soundcloud_master_player_liking = get_post_meta( $post->ID, 'soundcloud_master_player_liking', true );
	$soundcloud_master_player_download = get_post_meta( $post->ID, 'soundcloud_master_player_download', true );
	$soundcloud_master_player_sharing = get_post_meta( $post->ID, 'soundcloud_master_player_sharing', true );
	$soundcloud_master_player_artwork = get_post_meta( $post->ID, 'soundcloud_master_player_artwork', true );
	$soundcloud_master_player_comments = get_post_meta( $post->ID, 'soundcloud_master_player_comments', true );
	$soundcloud_master_player_playcount = get_post_meta( $post->ID, 'soundcloud_master_player_playcount', true );
	$soundcloud_master_player_showuser = get_post_meta( $post->ID, 'soundcloud_master_player_showuser', true );
	$soundcloud_master_player_bang = get_post_meta( $post->ID, 'soundcloud_master_player_bang', true );

?>
	<p>
	<img src="<?php echo plugins_url('../images/techgasp-minilogo-16.png', __FILE__); ?>" style="float:left; height:16px; vertical-align:middle;" />
	&nbsp;
	<input type="checkbox" <?php checked( (bool) $soundcloud_master_button_connect, true ); ?> id="soundcloud_master_button_connect" name="soundcloud_master_button_connect" />
	<label for="soundcloud_master_button_connect"><b><?php _e('Soundcloud Profile Button', 'soundcloud_master_shortcode'); ?></b></label>
	</p>
	<p>
	<label for="soundcloud_master_button_page_connect"><?php _e('Soundcloud Profile Link:', 'soundcloud_master_shortcode'); ?></label><br>
	<input id="soundcloud_master_button_page_connect" name="soundcloud_master_button_page_connect" value="<?php echo $soundcloud_master_button_page_connect ?>" style="width:300px;" />
	</p>
	<p>
	<img src="<?php echo plugins_url('../images/techgasp-minilogo-16.png', __FILE__); ?>" style="float:left; height:16px; vertical-align:middle;" />
	&nbsp;
	<input type="checkbox" <?php checked( (bool) $soundcloud_master_button_lyrics, true ); ?> id="soundcloud_master_button_lyrics" name="soundcloud_master_button_lyrics" />
	<label for="soundcloud_master_button_connect_lyrics"><b><?php _e('Soundcloud Lyrics Button', 'soundcloud_master_shortcode'); ?></b></label>
	</p>
	<p>
	<label for="soundcloud_master_button_page_lyrics"><?php _e('Soundcloud Lyrics Link:', 'soundcloud_master_shortcode'); ?></label><br>
	<input id="soundcloud_master_button_page_lyrics" name="soundcloud_master_button_page_lyrics" value="<?php echo $soundcloud_master_button_page_lyrics ?>" style="width:300px;" />
	</p>
<div style="background: url(<?php echo plugins_url('../images/techgasp-hr.png', __FILE__); ?>) repeat-x; height: 10px"></div>
	<p>
	<img src="<?php echo plugins_url('../images/techgasp-minilogo-16.png', __FILE__); ?>" style="float:left; height:16px; vertical-align:middle;" />
	&nbsp;
	<input type="checkbox" <?php checked( (bool) $soundcloud_master_player, true ); ?> id="soundcloud_master_player" name="soundcloud_master_player" />
	<label for="soundcloud_master_player"><b><?php _e('Show Soundcloud Player', 'soundcloud_master_shortcode'); ?></b></label>
	</p>
	<p>
	<label for="soundcloud_master_player_page"><?php _e('SoundClound Track or Set of Tracks Link:', 'soundcloud_master_shortcode'); ?></label><br>
	<input id="soundcloud_master_player_page" name="soundcloud_master_player_page" value="<?php echo $soundcloud_master_player_page ?>" style="width:300px;" />
	</p>
	<div class="description">Copy and Paste from your browser the link from a single track or set of tracks</div>
	<div class="description">More about these settings, <a href="http://wordpress.techgasp.com/soundcloud-master-documentation/" target="_blank">documentation</a>.</div>
	<p>
	<img src="<?php echo plugins_url('../images/techgasp-minilogo-16.png', __FILE__); ?>" style="float:left; height:16px; vertical-align:middle;" />
	&nbsp;
	<label for="soundcloud_master_align"><b><?php _e('Soundcloud Master Player Settings: ', 'soundcloud_master_shortcode'); ?></b></label>
	</p>
	<p>
	<input id="soundcloud_master_player_align" name="soundcloud_master_align" value="<?php echo $soundcloud_master_align ?>" style="width:60px;" />
	<label for="soundcloud_master_align"><?php _e('left or right, leave blank for none', 'soundcloud_master_shortcode'); ?></label>
	</p>
	<p>
	<input id="soundcloud_master_player_width" name="soundcloud_master_player_width" value="<?php echo $soundcloud_master_player_width ?>" style="width:60px;" />
	<label for="soundcloud_master_player_width"><?php _e('Soundcloud Player Width', 'soundcloud_master_shortcode'); ?></label>
	</p>
	<p>
	<div class="description">Default width is: <b>100%</b> for Desktop or Mobile Responsiveness. This value can be set to something else example, <b>350</b>. Any value different from 100% might affect mobile responsiveness.</div>
	</p>
	<p>
	<input id="soundcloud_master_player_height" name="soundcloud_master_player_height" value="<?php echo $soundcloud_master_player_height ?>" style="width:60px;" />
	<label for="soundcloud_master_player_height"><?php _e('Soundcloud Player Height', 'soundcloud_master_shortcode'); ?></label>
	</p>
	<p>
	<div class="description">Default is height is: <b>450</b>. You can play with this value, it will NOT affect mobile responsiveness.</div>
	</p>
	<p>
	<input id="soundcloud_master_player_color" name="soundcloud_master_player_color" value="<?php echo $soundcloud_master_player_color ?>" style="width:60px;" />
	<label for="soundcloud_master_player_color"><?php _e('Soundcloud Player Color', 'soundcloud_master_shortcode'); ?></label>
	</p>
	<p>
	<div class="description">Example, <b>ff5500</b> More colors: <a href="http://www.colorpicker.com/" target="_blank">Color Picker</a> Leave blank for default Soundcloud Color.</div>
	</p>
	<p>
	<img src="<?php echo plugins_url('../images/techgasp-minilogo-16.png', __FILE__); ?>" style="float:left; height:16px; vertical-align:middle;" />
	&nbsp;
	<input type="checkbox" <?php checked( (bool) $soundcloud_master_player_autoplay, true ); ?> id="soundcloud_master_player_autoplay" name="soundcloud_master_player_autoplay" />
	<label for="soundcloud_master_player_autoplay"><b><?php _e('Auto-Play', 'soundcloud_master_shortcode'); ?></b></label>
	</p>
	<p>
	<img src="<?php echo plugins_url('../images/techgasp-minilogo-16.png', __FILE__); ?>" style="float:left; height:16px; vertical-align:middle;" />
	&nbsp;
	<input type="checkbox" <?php checked( (bool) $soundcloud_master_player_showuser, true ); ?> id="soundcloud_master_player_showuser" name="soundcloud_master_player_showuser" />
	<label for="soundcloud_master_player_showuser"><b><?php _e('Display User', 'soundcloud_master_shortcode'); ?></b></label>
	</p>
	<p>
	<img src="<?php echo plugins_url('../images/techgasp-minilogo-16.png', __FILE__); ?>" style="float:left; height:16px; vertical-align:middle;" />
	&nbsp;
	<input type="checkbox" <?php checked( (bool) $soundcloud_master_player_artwork, true ); ?> id="soundcloud_master_player_artwork" name="soundcloud_master_player_artwork" />
	<label for="soundcloud_master_player_artwork"><b><?php _e('Display Artwork', 'soundcloud_master_shortcode'); ?></b></label>
	</p>
	<p>
	<img src="<?php echo plugins_url('../images/techgasp-minilogo-16.png', __FILE__); ?>" style="float:left; height:16px; vertical-align:middle;" />
	&nbsp;
	<input type="checkbox" <?php checked( (bool) $soundcloud_master_player_comments, true ); ?> id="soundcloud_master_player_comments" name="soundcloud_master_player_comments" />
	<label for="soundcloud_master_player_comments"><b><?php _e('Display Comments', 'soundcloud_master_shortcode'); ?></b></label>
	</p>
	<p>
	<img src="<?php echo plugins_url('../images/techgasp-minilogo-16.png', __FILE__); ?>" style="float:left; height:16px; vertical-align:middle;" />
	&nbsp;
	<input type="checkbox" <?php checked( (bool) $soundcloud_master_player_playcount, true ); ?> id="soundcloud_master_player_playcount" name="soundcloud_master_player_playcount" />
	<label for="soundcloud_master_player_playcount"><b><?php _e('Display Playcount', 'soundcloud_master_shortcode'); ?></b></label>
	</p>
	<p>
	<img src="<?php echo plugins_url('../images/techgasp-minilogo-16.png', __FILE__); ?>" style="float:left; height:16px; vertical-align:middle;" />
	&nbsp;
	<input type="checkbox" <?php checked( (bool) $soundcloud_master_player_sharing, true ); ?> id="soundcloud_master_player_sharing" name="soundcloud_master_player_sharing" />
	<label for="soundcloud_master_player_sharing"><b><?php _e('Display Share', 'soundcloud_master_shortcode'); ?></b></label>
	</p>
	<p>
	<img src="<?php echo plugins_url('../images/techgasp-minilogo-16.png', __FILE__); ?>" style="float:left; height:16px; vertical-align:middle;" />
	&nbsp;
	<input type="checkbox" <?php checked( (bool) $soundcloud_master_player_liking, true ); ?> id="soundcloud_master_player_liking" name="soundcloud_master_player_liking" />
	<label for="soundcloud_master_player_liking"><b><?php _e('Display Like', 'soundcloud_master_shortcode'); ?></b></label>
	</p>
	<p>
	<img src="<?php echo plugins_url('../images/techgasp-minilogo-16.png', __FILE__); ?>" style="float:left; height:16px; vertical-align:middle;" />
	&nbsp;
	<input type="checkbox" <?php checked( (bool) $soundcloud_master_player_download, true ); ?> id="soundcloud_master_player_download" name="soundcloud_master_player_download" />
	<label for="soundcloud_master_player_download"><b><?php _e('Display Download', 'soundcloud_master_shortcode'); ?></b></label>
	</p>
	<p>
	<img src="<?php echo plugins_url('../images/techgasp-minilogo-16.png', __FILE__); ?>" style="float:left; height:16px; vertical-align:middle;" />
	&nbsp;
	<input type="checkbox" <?php checked( (bool) $soundcloud_master_player_buying, true ); ?> id="soundcloud_master_player_buying" name="soundcloud_master_player_buying" />
	<label for="soundcloud_master_player_buying"><b><?php _e('Display Buy', 'soundcloud_master_shortcode'); ?></b></label>
	</p>
	<p>
	<img src="<?php echo plugins_url('../images/techgasp-minilogo-16.png', __FILE__); ?>" style="float:left; height:16px; vertical-align:middle;" />
	&nbsp;
	<input type="checkbox" <?php checked( (bool) $soundcloud_master_player_bang, true ); ?> id="soundcloud_master_player_bang" name="soundcloud_master_player_bang" />
	<label for="soundcloud_master_player_bang"><b><?php _e('TechGasp BANG! Artwork', 'soundcloud_master_shortcode'); ?></b></label>
	</p>
	<p>
	<div class="description">Activate to test, for users with great Cover Artworks. Some of the above player options will be disabled.</div>
	</p>
<div style="background: url(<?php echo plugins_url('../images/techgasp-hr.png', __FILE__); ?>) repeat-x; height: 10px"></div>
	<p>
	<img src="<?php echo plugins_url('../images/techgasp-minilogo-16.png', __FILE__); ?>" style="float:left; height:16px; vertical-align:middle;" />
	&nbsp;
	<input type="checkbox" <?php checked( (bool) $soundcloud_master_preview, true ); ?> id="soundcloud_master_preview" name="soundcloud_master_preview" />
	<label for="soundcloud_master_preview"><b><?php _e('Soundcloud Master Preview: ', 'soundcloud_master_shortcode'); ?></b></label>
	</p>
<?php
if ( $soundcloud_master_preview ){
	$url_loc = plugins_url();
	//Prepare Player Color
	if (empty ($soundcloud_master_player_color)){
		$soundcloud_master_player_color = "ff5500";
		}
	//Prepare Auto-Play
	if ( $soundcloud_master_player_autoplay ){
		$soundcloud_master_player_autoplay = "true";
	}
	else{
		$soundcloud_master_player_autoplay = "false";
	}
	//Prepare UserName
	if ( $soundcloud_master_player_showuser ){
		$soundcloud_master_player_showuser = "true";
	}
	else{
		$soundcloud_master_player_showuser = "false";
	}
	//Prepare Artwork
	if ( $soundcloud_master_player_artwork ){
		$soundcloud_master_player_artwork = "true";
	}
	else{
		$soundcloud_master_player_artwork = "false";
	}
	//Prepare Comments
	if ( $soundcloud_master_player_comments ){
		$soundcloud_master_player_comments = "true";
	}
	else{
		$soundcloud_master_player_comments = "false";
	}
	//Prepare Playcount
	if ( $soundcloud_master_player_playcount ){
		$soundcloud_master_player_playcount = "true";
	}
	else{
		$soundcloud_master_player_playcount = "false";
	}
	//Prepare Share
	if ( $soundcloud_master_player_sharing ){
		$soundcloud_master_player_sharing = "true";
	}
	else{
		$soundcloud_master_player_sharing = "false";
	}
	//Prepare Like
	if ( $soundcloud_master_player_liking ){
		$soundcloud_master_player_liking = "true";
	}
	else{
		$soundcloud_master_player_liking = "false";
	}
	//Prepare Download
	if ( $soundcloud_master_player_download ){
		$soundcloud_master_player_download = "true";
	}
	else{
		$soundcloud_master_player_download = "false";
	}
	//Prepare Buy
	if ( $soundcloud_master_player_buying ){
		$soundcloud_master_player_buying = "true";
	}
	else{
		$soundcloud_master_player_buying = "false";
	}
	//Prepare Bang
	if ( $soundcloud_master_player_bang ){
		$soundcloud_master_player_bang = "&amp;hide_related=true&amp;visual=true";
	}
	else{
		$soundcloud_master_player_bang = '';
	}
		//DISPLAY PLAYER
		if ( $soundcloud_master_player ){
		$soundcloud_master_create_player = '<iframe width="'.$soundcloud_master_player_width.'" height="'.$soundcloud_master_player_height.'" scrolling="no" frameborder="no" src="https://w.soundcloud.com/player/?url='.$soundcloud_master_player_page.''.$soundcloud_master_player_bang.'&amp;color='.$soundcloud_master_player_color.'&amp;auto_play='.$soundcloud_master_player_autoplay.'&amp;show_artwork='.$soundcloud_master_player_artwork.'&amp;show_user='.$soundcloud_master_player_showuser.'&amp;show_comments='.$soundcloud_master_player_comments.'&amp;show_playcount='.$soundcloud_master_player_playcount.'&amp;sharing='.$soundcloud_master_player_sharing.'&amp;liking='.$soundcloud_master_player_liking.'&amp;download='.$soundcloud_master_player_download.'&amp;buying='.$soundcloud_master_player_buying.'"></iframe>';
		}
		else{
		$soundcloud_master_create_player = '';
		}
		//DISPLAY CONNECT BUTTON
		if ( $soundcloud_master_button_connect ){
		$soundcloud_master_create_button_connect = '<a href="'.$soundcloud_master_button_page_connect.'" target="_blank"><img src="'.$url_loc.'/soundcloud-master/images/btn-connect-s.png"></a>&nbsp;';
		}
		else{
		$soundcloud_master_create_button_connect = '';
		}
		//DISPLAY LYRICS BUTTON
		if ( $soundcloud_master_button_lyrics ){
		$soundcloud_master_create_button_lyrics = '<a href="'.$soundcloud_master_button_page_lyrics.'" target="_blank"><img src="'.$url_loc.'/soundcloud-master/images/btn-lyrics.png"></a>';
		}
		else{
		$soundcloud_master_create_button_lyrics = '';
		}
echo '<div style="position: relative; float: '.$soundcloud_master_align.'; width: '.$soundcloud_master_player_width.'px; height: '.$soundcloud_master_player_height.'px; padding:8px; padding-bottom:35px !important;">' .
	$soundcloud_master_create_player .
	'<div style="padding-top:3px;">' .
	$soundcloud_master_create_button_connect . $soundcloud_master_create_button_lyrics .
	'</div>' .
	'</div>';
}
?>
	<h2><img src="<?php echo plugins_url('../images/techgasp-minilogo.png', __FILE__); ?>" style="width:40px; vertical-align:middle;" alt="' . esc_attr__( 'TechGasp Plugins') . '" /><b><?php echo get_option('soundcloud_master_name'); ?> Website</b></h2>
	<p><a class="button-secondary" href="http://wordpress.techgasp.com/soundcloud-master/" target="_blank" title="<?php echo get_option('soundcloud_master_name'); ?> Info Page">Info Page</a> <a class="button-secondary" href="http://wordpress.techgasp.com/soundcloud-master-documentation/" target="_blank" title="<?php echo get_option('soundcloud_master_name'); ?> Documentation">Documentation</a> <a class="button-primary" href="http://wordpress.org/plugins/soundcloud-master/" target="_blank" title="<?php echo get_option('soundcloud_master_name'); ?> Wordpress">RATE US *****</a></p>
<?php
}
/**
* When the post is saved, saves our custom data.
*
* @param int $post_id The ID of the post being saved.
*/
function soundcloud_master_save_postdata( $post_id ) {

/*
* We need to verify this came from the our screen and with proper authorization,
* because save_post can be triggered at other times.
*/

// Check if our nonce is set.
	if ( ! isset( $_POST['soundcloud_master_inner_custom_box_nonce'] ) )
	return $post_id;

	$nonce = $_POST['soundcloud_master_inner_custom_box_nonce'];

// Verify that the nonce is valid.
	if ( ! wp_verify_nonce( $nonce, 'soundcloud_master_inner_custom_box' ) )
	return $post_id;

// If this is an autosave, our form has not been submitted, so we don't want to do anything.
	if ( defined( 'DOING_AUTOSAVE' ) && DOING_AUTOSAVE )
	return $post_id;

// Check the user's permissions.
	if ( 'page' == $_POST['post_type'] ) {

	if ( ! current_user_can( 'edit_page', $post_id ) )
	return $post_id;

}
else {
	if ( ! current_user_can( 'edit_post', $post_id ) )
	return $post_id;
}

/* OK, its safe for us to save the data now. */

// Sanitize user input.
	@$soundcloud_master_preview = sanitize_text_field( $_POST['soundcloud_master_preview'] );
	@$soundcloud_master_align = sanitize_text_field( $_POST['soundcloud_master_align'] );
	@$soundcloud_master_button_connect = sanitize_text_field( $_POST['soundcloud_master_button_connect'] );
	@$soundcloud_master_button_page_connect = sanitize_text_field( $_POST['soundcloud_master_button_page_connect'] );
	@$soundcloud_master_button_lyrics = sanitize_text_field( $_POST['soundcloud_master_button_lyrics'] );
	@$soundcloud_master_button_page_lyrics = sanitize_text_field( $_POST['soundcloud_master_button_page_lyrics'] );
	@$soundcloud_master_player = sanitize_text_field( $_POST['soundcloud_master_player'] );
	@$soundcloud_master_player_page = $_POST['soundcloud_master_player_page'];
	@$soundcloud_master_player_width = sanitize_text_field( $_POST['soundcloud_master_player_width'] );
	@$soundcloud_master_player_height = sanitize_text_field( $_POST['soundcloud_master_player_height'] );
	@$soundcloud_master_player_color = sanitize_text_field( $_POST['soundcloud_master_player_color'] );
	@$soundcloud_master_player_autoplay = sanitize_text_field( $_POST['soundcloud_master_player_autoplay'] );
	@$soundcloud_master_player_buying = sanitize_text_field( $_POST['soundcloud_master_player_buying'] );
	@$soundcloud_master_player_liking = sanitize_text_field( $_POST['soundcloud_master_player_liking'] );
	@$soundcloud_master_player_download = sanitize_text_field( $_POST['soundcloud_master_player_download'] );
	@$soundcloud_master_player_sharing = sanitize_text_field( $_POST['soundcloud_master_player_sharing'] );
	@$soundcloud_master_player_artwork = sanitize_text_field( $_POST['soundcloud_master_player_artwork'] );
	@$soundcloud_master_player_comments = sanitize_text_field( $_POST['soundcloud_master_player_comments'] ); 
	@$soundcloud_master_player_playcount = sanitize_text_field( $_POST['soundcloud_master_player_playcount'] );
	@$soundcloud_master_player_showuser = sanitize_text_field( $_POST['soundcloud_master_player_showuser'] );
	@$soundcloud_master_player_bang = sanitize_text_field( $_POST['soundcloud_master_player_bang'] );

// Update the meta field in the database.
	update_post_meta( $post_id, 'soundcloud_master_preview', $soundcloud_master_preview );
	update_post_meta( $post_id, 'soundcloud_master_align', $soundcloud_master_align );
	update_post_meta( $post_id, 'soundcloud_master_button_connect', $soundcloud_master_button_connect );
	update_post_meta( $post_id, 'soundcloud_master_button_page_connect', $soundcloud_master_button_page_connect );
	update_post_meta( $post_id, 'soundcloud_master_button_lyrics', $soundcloud_master_button_lyrics );
	update_post_meta( $post_id, 'soundcloud_master_button_page_lyrics', $soundcloud_master_button_page_lyrics );
	update_post_meta( $post_id, 'soundcloud_master_player', $soundcloud_master_player );
	update_post_meta( $post_id, 'soundcloud_master_player_page', $soundcloud_master_player_page );
	update_post_meta( $post_id, 'soundcloud_master_player_width', $soundcloud_master_player_width );
	update_post_meta( $post_id, 'soundcloud_master_player_height', $soundcloud_master_player_height );
	update_post_meta( $post_id, 'soundcloud_master_player_color', $soundcloud_master_player_color );
	update_post_meta( $post_id, 'soundcloud_master_player_autoplay', $soundcloud_master_player_autoplay );
	update_post_meta( $post_id, 'soundcloud_master_player_buying', $soundcloud_master_player_buying );
	update_post_meta( $post_id, 'soundcloud_master_player_liking', $soundcloud_master_player_liking );
	update_post_meta( $post_id, 'soundcloud_master_player_download', $soundcloud_master_player_download );
	update_post_meta( $post_id, 'soundcloud_master_player_sharing', $soundcloud_master_player_sharing );
	update_post_meta( $post_id, 'soundcloud_master_player_artwork', $soundcloud_master_player_artwork );
	update_post_meta( $post_id, 'soundcloud_master_player_comments', $soundcloud_master_player_comments );
	update_post_meta( $post_id, 'soundcloud_master_player_playcount', $soundcloud_master_player_playcount );
	update_post_meta( $post_id, 'soundcloud_master_player_showuser', $soundcloud_master_player_showuser );
	update_post_meta( $post_id, 'soundcloud_master_player_bang', $soundcloud_master_player_bang );
}
add_action( 'save_post', 'soundcloud_master_save_postdata' );

//////////////////////////
// SHORTCODE START HERE //
//////////////////////////
function soundcloud_master_add_shortcode( $atts) {
global $post;

extract( shortcode_atts( array(
'soundcloud_master_align'				=> 'soundcloud_master_align',
'soundcloud_master_button_connect'		=> 'soundcloud_master_button_connect',
'soundcloud_master_button_page_connect'	=> 'soundcloud_master_button_page_connect',
'soundcloud_master_button_lyrics'		=> 'soundcloud_master_button_lyrics',
'soundcloud_master_button_page_lyrics'	=> 'soundcloud_master_button_page_lyrics',
'soundcloud_master_player'				=> 'soundcloud_master_player',
'soundcloud_master_player_page'			=> 'soundcloud_master_player_page',
'soundcloud_master_player_width'		=> 'soundcloud_master_player_width', 
'soundcloud_master_player_height'		=> 'soundcloud_master_player_height',
'soundcloud_master_player_color'		=> 'soundcloud_master_player_color',
'soundcloud_master_player_autoplay'		=> 'soundcloud_master_player_autoplay',
'soundcloud_master_player_buying'		=> 'soundcloud_master_player_buying',
'soundcloud_master_player_liking'		=> 'soundcloud_master_player_liking',
'soundcloud_master_player_download'		=> 'soundcloud_master_player_download',
'soundcloud_master_player_sharing'		=> 'soundcloud_master_player_sharing',
'soundcloud_master_player_artwork'		=> 'soundcloud_master_player_artwork',
'soundcloud_master_player_comments'		=> 'soundcloud_master_player_comments',
'soundcloud_master_player_playcount'		=> 'soundcloud_master_player_playcount',
'soundcloud_master_player_showuser'		=> 'soundcloud_master_player_showuser',
'soundcloud_master_player_bang'		=> 'soundcloud_master_player_bang'
), $atts ) );

$soundcloud_master_align = get_post_meta($post->ID, $soundcloud_master_align, true);
$soundcloud_master_button_connect = get_post_meta($post->ID, $soundcloud_master_button_connect, true);
$soundcloud_master_button_page_connect = get_post_meta($post->ID, $soundcloud_master_button_page_connect, true);
$soundcloud_master_button_lyrics = get_post_meta($post->ID, $soundcloud_master_button_lyrics, true);
$soundcloud_master_button_page_lyrics = get_post_meta($post->ID, $soundcloud_master_button_page_lyrics, true);
$soundcloud_master_player = get_post_meta($post->ID, $soundcloud_master_player, true);
$soundcloud_master_player_page = get_post_meta($post->ID, $soundcloud_master_player_page, true);
$soundcloud_master_player_width = get_post_meta($post->ID, $soundcloud_master_player_width, true);
$soundcloud_master_player_height = get_post_meta( $post->ID, 'soundcloud_master_player_height', true );
$soundcloud_master_player_color = get_post_meta( $post->ID, 'soundcloud_master_player_color', true );
$soundcloud_master_player_autoplay = get_post_meta( $post->ID, 'soundcloud_master_player_autoplay', true );
$soundcloud_master_player_buying = get_post_meta( $post->ID, 'soundcloud_master_player_buying', true );
$soundcloud_master_player_liking = get_post_meta( $post->ID, 'soundcloud_master_player_liking', true );
$soundcloud_master_player_download = get_post_meta( $post->ID, 'soundcloud_master_player_download', true );
$soundcloud_master_player_sharing = get_post_meta( $post->ID, 'soundcloud_master_player_sharing', true );
$soundcloud_master_player_artwork = get_post_meta( $post->ID, 'soundcloud_master_player_artwork', true );
$soundcloud_master_player_comments = get_post_meta( $post->ID, 'soundcloud_master_player_comments', true );
$soundcloud_master_player_playcount = get_post_meta( $post->ID, 'soundcloud_master_player_playcount', true );
$soundcloud_master_player_showuser = get_post_meta( $post->ID, 'soundcloud_master_player_showuser', true );
$soundcloud_master_player_bang = get_post_meta( $post->ID, 'soundcloud_master_player_bang', true );

	$url_loc = plugins_url();
	//Prepare Player Color
	if (empty ($soundcloud_master_player_color)){
		$soundcloud_master_player_color = "ff5500";
		}
	//Prepare Auto-Play
	if ( $soundcloud_master_player_autoplay ){
		$soundcloud_master_player_autoplay = "true";
	}
	else{
		$soundcloud_master_player_autoplay = "false";
	}
	//Prepare UserName
	if ( $soundcloud_master_player_showuser ){
		$soundcloud_master_player_showuser = "true";
	}
	else{
		$soundcloud_master_player_showuser = "false";
	}
	//Prepare Artwork
	if ( $soundcloud_master_player_artwork ){
		$soundcloud_master_player_artwork = "true";
	}
	else{
		$soundcloud_master_player_artwork = "false";
	}
	//Prepare Comments
	if ( $soundcloud_master_player_comments ){
		$soundcloud_master_player_comments = "true";
	}
	else{
		$soundcloud_master_player_comments = "false";
	}
	//Prepare Playcount
	if ( $soundcloud_master_player_playcount ){
		$soundcloud_master_player_playcount = "true";
	}
	else{
		$soundcloud_master_player_playcount = "false";
	}
	//Prepare Share
	if ( $soundcloud_master_player_sharing ){
		$soundcloud_master_player_sharing = "true";
	}
	else{
		$soundcloud_master_player_sharing = "false";
	}
	//Prepare Like
	if ( $soundcloud_master_player_liking ){
		$soundcloud_master_player_liking = "true";
	}
	else{
		$soundcloud_master_player_liking = "false";
	}
	//Prepare Download
	if ( $soundcloud_master_player_download ){
		$soundcloud_master_player_download = "true";
	}
	else{
		$soundcloud_master_player_download = "false";
	}
	//Prepare Buy
	if ( $soundcloud_master_player_buying ){
		$soundcloud_master_player_buying = "true";
	}
	else{
		$soundcloud_master_player_buying = "false";
	}
	//Prepare Bang
	if ( $soundcloud_master_player_bang ){
		$soundcloud_master_player_bang = "&amp;hide_related=true&amp;visual=true";
	}
	else{
		$soundcloud_master_player_bang = '';
	}
		//DISPLAY PLAYER
		if ( $soundcloud_master_player ){
		$soundcloud_master_create_player = '<iframe width="'.$soundcloud_master_player_width.'" height="'.$soundcloud_master_player_height.'" scrolling="no" frameborder="no" src="https://w.soundcloud.com/player/?url='.$soundcloud_master_player_page.''.$soundcloud_master_player_bang.'&amp;color='.$soundcloud_master_player_color.'&amp;auto_play='.$soundcloud_master_player_autoplay.'&amp;show_artwork='.$soundcloud_master_player_artwork.'&amp;show_user='.$soundcloud_master_player_showuser.'&amp;show_comments='.$soundcloud_master_player_comments.'&amp;show_playcount='.$soundcloud_master_player_playcount.'&amp;sharing='.$soundcloud_master_player_sharing.'&amp;liking='.$soundcloud_master_player_liking.'&amp;download='.$soundcloud_master_player_download.'&amp;buying='.$soundcloud_master_player_buying.'"></iframe>';
		}
		else{
		$soundcloud_master_create_player = '';
		}
		//DISPLAY CONNECT BUTTON
		if ( $soundcloud_master_button_connect ){
		$soundcloud_master_create_button_connect = '<a href="'.$soundcloud_master_button_page_connect.'" target="_blank"><img src="'.$url_loc.'/soundcloud-master/images/btn-connect-s.png"></a>&nbsp;';
		}
		else{
		$soundcloud_master_create_button_connect = "";
		}
		//DISPLAY LYRICS BUTTON
		if ( $soundcloud_master_button_lyrics ){
		$soundcloud_master_create_button_lyrics = '<a href="'.$soundcloud_master_button_page_lyrics.'" target="_blank"><img src="'.$url_loc.'/soundcloud-master/images/btn-lyrics.png"></a>';
		}
		else{
		$soundcloud_master_create_button_lyrics = '';
		}

return '<div style="position: relative; float: '.$soundcloud_master_align.'; width: '.$soundcloud_master_player_width.'px; height: '.$soundcloud_master_player_height.'px; padding:8px; padding-bottom:25px !important;">'.
	$soundcloud_master_create_player .
	'<div style="padding-top:3px;">' .
	$soundcloud_master_create_button_connect . $soundcloud_master_create_button_lyrics .
	'</div>' .
	'</div>';
}

add_shortcode('soundcloud-master', 'soundcloud_master_add_shortcode');