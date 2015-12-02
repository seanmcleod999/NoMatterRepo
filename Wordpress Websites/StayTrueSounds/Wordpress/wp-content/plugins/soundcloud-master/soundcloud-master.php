<?php
/**
Plugin Name: SoundCloud Master
Plugin URI: http://wordpress.techgasp.com/soundcloud-master/
Version: 4.3.6
Author: TechGasp
Author URI: http://wordpress.techgasp.com
Text Domain: soundcloud-master
Description: SoundCloud Master is a light weight and shiny clean code wordpress plugin WIDGET that you need to show off and sell your music.
License: GPL2 or later
*/
/*  Copyright 2013 TechGasp  (email : info@techgasp.com)
	This program is free software; you can redistribute it and/or modify
	it under the terms of the GNU General Public License, version 2, as 
	published by the Free Software Foundation.

	This program is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with this program; if not, write to the Free Software
	Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/
if(!class_exists('soundcloud_master')) :
///////DEFINE ID//////
define('SOUNDCLOUD_MASTER_ID', 'soundcloud-master');
///////DEFINE VERSION///////
define( 'soundcloud_master_VERSION', '4.3.6' );
global $soundcloud_master_version, $soundcloud_master_name;
$soundcloud_master_version = "4.3.6"; //for other pages
$soundcloud_master_name = "Soundcloud Master"; //pretty name
if( is_multisite() ) {
update_site_option( 'soundcloud_master_installed_version', $soundcloud_master_version );
update_site_option( 'soundcloud_master_name', $soundcloud_master_name );
}
else{
update_option( 'soundcloud_master_installed_version', $soundcloud_master_version );
update_option( 'soundcloud_master_name', $soundcloud_master_name );
}
// HOOK ADMIN
require_once( dirname( __FILE__ ) . '/includes/soundcloud-master-admin.php');
// HOOK ADMIN IN & UN SHORTCODE
require_once( dirname( __FILE__ ) . '/includes/soundcloud-master-admin-shortcodes.php');
// HOOK ADMIN WIDGETS
require_once( dirname( __FILE__ ) . '/includes/soundcloud-master-admin-widgets.php');
// HOOK ADMIN ADDONS
require_once( dirname( __FILE__ ) . '/includes/soundcloud-master-admin-addons.php');
// HOOK ADMIN UPDATER
require_once( dirname( __FILE__ ) . '/includes/soundcloud-master-admin-updater.php');
// HOOK ADMIN FREE PLUGIN
require_once( dirname( __FILE__ ) . '/includes/soundcloud-master-admin-free.php');
// HOOK ADMIN INVITATION
require_once( dirname( __FILE__ ) . '/includes/soundcloud-master-invite.php');
// HOOK SHORTCODE IN
require_once( dirname( __FILE__ ) . '/includes/soundcloud-master-shortcode-in.php');
// HOOK SHORTCODE UN
require_once( dirname( __FILE__ ) . '/includes/soundcloud-master-shortcode-un.php');
// HOOK WIDGET BUTTONS
require_once( dirname( __FILE__ ) . '/includes/soundcloud-master-widget-buttons.php');
// HOOK WIDGET BASIC
require_once( dirname( __FILE__ ) . '/includes/soundcloud-master-widget-basic.php');
// HOOK WIDGET RESPONSIVE
require_once( dirname( __FILE__ ) . '/includes/soundcloud-master-widget-advanced.php');

class soundcloud_master{
//REGISTER PLUGIN
public static function soundcloud_master_register(){
register_setting(SOUNDCLOUD_MASTER_ID, 'tsm_quote');
}
public static function content_with_quote($content){
$quote = '<p>' . get_option('tsm_quote') . '</p>';
	return $content . $quote;
}
//SETTINGS LINK IN PLUGIN MANAGER
public static function soundcloud_master_links( $links, $file ) {
	if ( $file == plugin_basename( dirname(__FILE__).'/soundcloud-master.php' ) ) {
		$links[] = '<a href="' . admin_url( 'admin.php?page=soundcloud-master' ) . '">'.__( 'Settings' ).'</a>';
	}

	return $links;
}

public static function soundcloud_master_updater_version_check(){
global $soundcloud_master_version;
//CHECK NEW VERSION
$soundcloud_master_slug = basename(dirname(__FILE__));
$current = get_site_transient( 'update_plugins' );
$soundcloud_plugin_slug = $soundcloud_master_slug.'/'.$soundcloud_master_slug.'.php';
@$r = $current->response[ $soundcloud_plugin_slug ];
if (empty($r)){
$r = false;
$soundcloud_plugin_slug = false;
if( is_multisite() ) {
update_site_option( 'soundcloud_master_newest_version', $soundcloud_master_version );
}
else{
update_option( 'soundcloud_master_newest_version', $soundcloud_master_version );
}
}
if (!empty($r)){
$soundcloud_plugin_slug = $soundcloud_master_slug.'/'.$soundcloud_master_slug.'.php';
@$r = $current->response[ $soundcloud_plugin_slug ];
if( is_multisite() ) {
update_site_option( 'soundcloud_master_newest_version', $r->new_version );
}
else{
update_option( 'soundcloud_master_newest_version', $r->new_version );
}
}
}
// Advanced Updater
//Remove WP Updater
public static function soundcloud_master_remove_wp_updater( $r, $url ){
	if ( 0 !== strpos( $url, 'http://api.wordpress.org/plugins/update-check' ) )
	return $r; // Not a plugin update request. Bail immediately.
	$plugins = unserialize( $r['body']['plugins'] );
	unset( $plugins->plugins[ plugin_basename( __FILE__ ) ] );
	unset( $plugins->active[ array_search( plugin_basename( __FILE__ ), $plugins->active ) ] );
	$r['body']['plugins'] = serialize( $plugins );
	return $r;
}
public static function soundcloud_updater(){
global $soundcloud_api_url, $soundcloud_plugin_slug, $soundcloud_wp_version, $soundcloud_down_date, $soundcloud_down_admin_email, $soundcloud_down_blog, $soundcloud_down_slug, $soundcloud_down_link;
$soundcloud_api_url = 'http://wordpress.techgasp.com/extensionsadv/api/soundcloud_qu1xe/index_soundcloud.php';
$soundcloud_plugin_slug = basename(dirname(__FILE__));
$soundcloud_down_date = current_time('mysql');
$soundcloud_down_admin_email = get_option('admin_email');
$soundcloud_down_blog = get_site_url();
$soundcloud_down_slug = basename(dirname(__FILE__));
$soundcloud_down_link = get_option('down_link_soundcloud');
// Take over the update check
	add_filter('pre_set_site_transient_update_plugins', 'check_for_soundcloud_update');

function check_for_soundcloud_update($checked_data) {
global $soundcloud_api_url, $soundcloud_plugin_slug, $soundcloud_wp_version, $soundcloud_down_date, $soundcloud_down_admin_email, $soundcloud_down_blog, $soundcloud_down_slug, $soundcloud_down_link;
//Comment out these two lines during testing.
if (empty($checked_data->checked))
return $checked_data;

$args = array(
'slug' => $soundcloud_plugin_slug,
'version' => $checked_data->checked[$soundcloud_plugin_slug .'/'. $soundcloud_plugin_slug .'.php'],
);
$request_string = array(
	'body' => array(
	'action' => 'basic_check',
	'request' => serialize($args),
	'api-key' => md5(get_bloginfo('url')),
	'soundcloud_down_date' => $soundcloud_down_date,
	'soundcloud_down_admin_email' => $soundcloud_down_admin_email,
	'soundcloud_down_blog' => $soundcloud_down_blog,
	'soundcloud_down_slug' => $soundcloud_down_slug,
	'soundcloud_down_link' => $soundcloud_down_link
	),
	'user-agent' => 'WordPress/' . $soundcloud_wp_version . '; ' . get_bloginfo('url')
);

// Start checking for an update
$raw_response = wp_remote_post($soundcloud_api_url, $request_string);

if (!is_wp_error($raw_response) && ($raw_response['response']['code'] == 200))
$response = unserialize($raw_response['body']);

if (is_object($response) && !empty($response)) // Feed the update data into WP updater
$checked_data->response[$soundcloud_plugin_slug .'/'. $soundcloud_plugin_slug .'.php'] = $response;

return $checked_data;
}

// Take over the Plugin info screen
	add_filter('plugins_api', 'soundcloud_api_call', 10, 3);

function soundcloud_api_call($def, $action, $args) {
global $soundcloud_api_url, $soundcloud_plugin_slug, $soundcloud_wp_version, $soundcloud_down_date, $soundcloud_down_admin_email, $soundcloud_down_blog, $soundcloud_down_slug, $soundcloud_down_link;

if (!isset($args->slug) || ($args->slug != $soundcloud_plugin_slug))
return false;

// Get the current version
$plugin_info = get_site_transient('update_plugins');
$current_version = $plugin_info->checked[$soundcloud_plugin_slug .'/'. $soundcloud_plugin_slug .'.php'];
$args->version = $current_version;

$request_string = array(
	'body' => array(
	'action' => $action,
	'request' => serialize($args),
	'api-key' => md5(get_bloginfo('url')),
	'soundcloud_down_date' => $soundcloud_down_date,
	'soundcloud_down_admin_email' => $soundcloud_down_admin_email,
	'soundcloud_down_blog' => $soundcloud_down_blog,
	'soundcloud_down_slug' => $soundcloud_down_slug,
	'soundcloud_down_link' => $soundcloud_down_link
	),
	'user-agent' => 'WordPress/' . $soundcloud_wp_version . '; ' . get_bloginfo('url')
);

$request = wp_remote_post($soundcloud_api_url, $request_string);

if (is_wp_error($request)) {
$res = new WP_Error('plugins_api_failed', __('An Unexpected HTTP Error occurred during the API request.</p> <p><a href="?" onclick="document.location.reload(); return false;">Try again</a>'), $request->get_error_message());
} 
else {
$res = unserialize($request['body']);

if ($res === false)
$res = new WP_Error('plugins_api_failed', __('An unknown error occurred'), $request['body']);
}

return $res;
}
}
//Updater Label Message
public static function soundcloud_master_updater_message() {
$techgasp_updater_info1 = __( 'Important!', 'soundcloud_master' );
$techgasp_updater_info2 = __( ' Check if your download key is in place.', 'soundcloud_master' );
$techgasp_updater_info3 = ' <a href="admin.php?page=soundcloud-master-admin-updater">Updater Page</a>';
$techgasp_updater_icon = plugins_url('images/techgasp-updater-icon.png', __FILE__);
echo '<br><div style="width:28px; vertical-align:middle; float:left;"><img src='.$techgasp_updater_icon.'></div><b>'.$techgasp_updater_info1.'</b>'.$techgasp_updater_info2.$techgasp_updater_info3;
}
//END CLASS
}
if ( is_admin() ){
	add_action('admin_init', array('soundcloud_master', 'soundcloud_master_register'));
	add_action('init', array('soundcloud_master', 'soundcloud_master_updater_version_check'));
	add_action('init', array('soundcloud_master', 'soundcloud_updater'));
	add_action( 'in_plugin_update_message-' . plugin_basename(__FILE__), array('soundcloud_master', 'soundcloud_master_updater_message' ));
}
add_filter('http_request_args', array('soundcloud_master', 'soundcloud_master_remove_wp_updater'), 5, 2 );
add_filter('the_content', array('soundcloud_master', 'content_with_quote'));
add_filter( 'plugin_action_links', array('soundcloud_master', 'soundcloud_master_links'), 10, 2 );
endif;