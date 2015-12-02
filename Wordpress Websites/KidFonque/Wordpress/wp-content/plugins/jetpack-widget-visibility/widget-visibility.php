<?php

/*
 * Plugin Name: Jetpack Widget Visibility
 * Plugin URI: http://wordpress.org/plugins/jetpack-widget-visibility/
 * Description: Control what pages your widgets appear on.
 * Author: Anas H. Sulaiman
 * Version: 3.0.1
 * Author URI: http://ahs.pw/
 * Text Domain: jetpack
 * Domain Path: /languages/
 * License: GPL2 or later
 * License URI: http://www.gnu.org/licenses/gpl-2.0.html
 */

/**
 * Module Name: Widget Visibility
 * Module Description: Specify which widgets appear on which pages of your site.
 * First Introduced: 2.4
 * Requires Connection: No
 * Auto Activate: Yes
 * Sort Order: 17
 * Module Tags: Appearance
 */

include dirname( __FILE__ ) . "/widget-visibility/widget-conditions.php";

// E-1 {
function jetpack_widget_visibility_load_textdomain() {
	load_plugin_textdomain( 'jetpack', false, dirname( plugin_basename( __FILE__ ) ) . '/languages/' );
}
add_action( 'plugins_loaded', 'jetpack_widget_visibility_load_textdomain' );
// } E-1

// E-2 {
function jetpack_widget_visibility_settings_link($actions) {
	return array_merge(
		array( 'settings' => sprintf( '<a href="%s">%s</a>', 'widgets.php', __( 'Widgets', 'jetpack' ) ) ),
		$actions
	);
	return $actions;
}
add_filter('plugin_action_links_' . plugin_basename(__FILE__), 'jetpack_widget_visibility_settings_link' );
// } E-2

/*
Edits by Anas H. Sulaiman:
E-1 : load text domain
E-2 : add settings link
*/
