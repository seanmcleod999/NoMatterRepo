<?php
function soundcloud_master_shortcode_un(){
$url_loc = plugins_url();

//Prepare Auto-Play
if ( get_option('soundcloud_master_un_player_autoplay') == 'true' ){
$soundcloud_master_un_player_autoplay = "true";
}
else{
$soundcloud_master_un_player_autoplay = "false";
}

//Prepare UserName
if ( get_option('soundcloud_master_un_player_showuser') == 'true' ){
$soundcloud_master_un_player_showuser = "true";
}
else{
$soundcloud_master_un_player_showuser = "false";
}

//Prepare Artwork
if ( get_option('soundcloud_master_un_player_artwork') == 'true' ){
$soundcloud_master_un_player_artwork = "true";
}
else{
$soundcloud_master_un_player_artwork = "false";
}

//Prepare Comments
if ( get_option('soundcloud_master_un_player_comments') == 'true' ){
$soundcloud_master_un_player_comments = "true";
}
else{
$soundcloud_master_un_player_comments = "false";
}

//Prepare Playcount
if ( get_option('soundcloud_master_un_player_playcount') == 'true' ){
$soundcloud_master_un_player_playcount = "true";
}
else{
$soundcloud_master_un_player_playcount = "false";
}

//Prepare Share
if ( get_option('soundcloud_master_un_player_sharing') == 'true' ){
$soundcloud_master_un_player_sharing = "true";
}
else{
$soundcloud_master_un_player_sharing = "false";
}

//Prepare Like
if ( get_option('soundcloud_master_un_player_liking') == 'true' ){
$soundcloud_master_un_player_liking = "true";
}
else{
$soundcloud_master_un_player_liking = "false";
}

//Prepare Download
if ( get_option('soundcloud_master_un_player_download') == 'true' ){
$soundcloud_master_un_player_download = "true";
}
else{
$soundcloud_master_un_player_download = "false";
}

//Prepare Buy
if ( get_option('soundcloud_master_un_player_buying') == 'true' ){
$soundcloud_master_un_player_buying = "true";
}
else{
$soundcloud_master_un_player_buying = "false";
}

//Prepare Bang
if ( get_option('soundcloud_master_un_player_bang') == 'true' ){
$soundcloud_master_un_player_bang_create = "&amp;hide_related=true&amp;visual=true";
}
else{
$soundcloud_master_un_player_bang_create = '';
}

//DISPLAY PLAYER
if ( get_option('soundcloud_master_un_player') == 'true' ){
$soundcloud_master_un_create_player = '<iframe width="100%" height="'.get_option('soundcloud_master_un_player_height').'" scrolling="no" frameborder="no" src="https://w.soundcloud.com/player/?url='.get_option('soundcloud_master_un_player_page').''.$soundcloud_master_un_player_bang_create.'&amp;color=ff550&amp;auto_play='.$soundcloud_master_un_player_autoplay.'&amp;show_artwork='.$soundcloud_master_un_player_artwork.'&amp;show_user='.$soundcloud_master_un_player_showuser.'&amp;show_comments='.$soundcloud_master_un_player_comments.'&amp;show_playcount='.$soundcloud_master_un_player_playcount.'&amp;sharing='.$soundcloud_master_un_player_sharing.'&amp;liking='.$soundcloud_master_un_player_liking.'&amp;download='.$soundcloud_master_un_player_download.'&amp;buying='.$soundcloud_master_un_player_buying.'"></iframe>';
}
else{
$soundcloud_master_un_create_player = false;
}

//DISPLAY CONNECT BUTTON
if ( get_option('soundcloud_master_un_button_connect') == 'true' ){
$soundcloud_master_un_create_button_connect = '<a href="'.get_option('soundcloud_master_un_button_page_connect').'" target="_blank"><img src="'.$url_loc.'/soundcloud-master/images/btn-connect-s.png"></a>&nbsp;';
}
else{
$soundcloud_master_un_create_button_connect = false;
}

//DISPLAY LYRICS BUTTON
if ( get_option('soundcloud_master_un_button_lyrics') == 'true' ){
$soundcloud_master_un_create_button_lyrics = '<a href="'.get_option('soundcloud_master_un_button_page_lyrics').'" target="_blank"><img src="'.$url_loc.'/soundcloud-master/images/btn-lyrics.png"></a>';
}
else{
$soundcloud_master_un_create_button_lyrics = false;
}

	return '<div style="width: 100%; height: '.get_option('soundcloud_master_un_player_height').'px; padding:8px; padding-bottom:25px !important;">'.
	$soundcloud_master_un_create_player .
	'<div style="padding-top:3px;">' .
	$soundcloud_master_un_create_button_connect . $soundcloud_master_un_create_button_lyrics .
	'</div>' .
	'</div>';
}
add_shortcode('soundcloud-master-un', 'soundcloud_master_shortcode_un');

//////////////////////
//AUTO ADD SHORTCODE//
//////////////////////
function soundcloud_master_shortcode_auto_content($content){
//IF AUTO ON
	if ( get_option('soundcloud_master_un_auto') == 'true' ){
//IF POSTS ONLY
		if ( get_option('soundcloud_master_un_auto_posts') == 'true' ){
			if(is_single()) {
//IS POSTS ONLY AFTER TITLE
				if ( get_option('soundcloud_master_un_auto_top') == 'true' ){
					$custom_content = soundcloud_master_shortcode_un();
					$content = $custom_content. $content;
					return $content;
				}
//IS POSTS ONLY AFTER CONTENT
				else{
						$custom_content = soundcloud_master_shortcode_un();
						$content .= $custom_content;
						return $content;
				}
			}
			else{
//IF POSTS ONLY RETURNS NO SHORTCODE PAGE
			return $content;
			}
		}
//IF POSTS AND PAGES
		else{
//IS POSTS AND PAGES AFTER TITLE
				if ( get_option('soundcloud_master_un_auto_top') == 'true' ){
						$custom_content = soundcloud_master_shortcode_un();
						$content = $custom_content. $content;
						return $content;
				}
//IS POSTS AND PAGES AFTER CONTENT
				else{
						$custom_content = soundcloud_master_shortcode_un();
						$content .= $custom_content;
						return $content;
					}
		}
	}
//IF AUTO OFF
	else{
return $content;
	}
}
add_filter ('the_content', 'soundcloud_master_shortcode_auto_content');
?>