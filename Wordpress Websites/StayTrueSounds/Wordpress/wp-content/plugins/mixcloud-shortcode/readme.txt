=== Mixcloud Shortcode ===
Contributors: zsantana
Donate link: http://wordpress.org/plugins/mixcloud-shortcode/
Tags: mixcloud, shortcode, player, flash
Requires at least: 2.5.0
Tested up to: 3.5.1
Stable tag: trunk
License: GPLv2 or later
License URI: http://www.gnu.org/licenses/gpl-2.0.html

Mixcloud Shortcode to add the player to your blog or page.

== Description ==

This plugin allows you to add the Mixcloud player into your WordPress blog or page, by using the [mixcloud] shortcode.

= Usage =

Copy the URL of the song you wish to add to your WordPress post or page. 

Paste it between *[mixcloud]* and *[/mixcloud]*

`[mixcloud]

http://www.mixcloud.com/essential/essential-podcast-conception-041/

[/mixcloud]`
 
The optional parameters are height and width: 

`[mixcloud height="100" width="400"]

	http://www.mixcloud.com/artist-name/recorded-live/

[/mixcloud]`

= Parameters =

*	Height: integer value
*	Width: integer value

= Examples =

`[mixcloud]

	http://www.mixcloud.com/essential/essential-podcast-conception-041/[/mixcloud]

[mixcloud height="100" width="400"]`


== Installation ==

This section describes how to install the plugin and get it working.

1. Download `mixcloud-shortcode.zip` and unzip it.
2. Upload the folder 'mixcloud-shortcode' to the `/wp-content/plugins/` directory
3. Activate the plugin through the 'Plugins' menu in WordPress
4. Place [mixcloud]link goes here[/mixcloud] in your post or page
5. E.g.

`[mixcloud]

http://www.mixcloud.com/essential/essential-podcast-conception-041/

[/mixcloud]`

== Frequently Asked Questions ==

Shortcode to add the Mixcloud player to your blog/page.

== Screenshots ==

1. Mixcloud player on a post

== Changelog ==

= 1.2 =
* Firefox is alright

= 1.1 =
* Fixing major parser bug

= 1.0 =
* First version
