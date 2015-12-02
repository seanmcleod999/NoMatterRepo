=== Jetpack Widget Visibility ===
Contributors: ahspw
Tags: widget, visibility, show, hide, control, jetpack, widgets
Requires at least: 3.5
Tested up to: 3.9.1
Stable tag: 3.0.1
License: GPLv2 or later
License URI: http://www.gnu.org/licenses/gpl-2.0.html

Specify which widgets appear on which pages of your site.

== Description ==

Jetpack Widget Visibility adds a new button to every widget in the widget area, allowing you to choose on which page this particular widget appears. or disappears.  
It's very easy to add conditions and the conditions are flexibile enough to give you great control over you widgets visibility.

Visibility is controlled by five aspects: page type, category, tag, date, and author.  
For example, if you wanted the Archives widget to only appear on category archives and error pages, choose “Show” from the first dropdown and then add two rules: “Page is 404 Error Page” and “Category is All Category Pages.”

You can also hide widgets based on the current page.  
For example, if you don’t want the Archives widget to appear on search results pages, choose “Hide” and “Page is Search Results.”

= Before asking for help =

* If this plugin works (which means it activates without problems), please post your help request on the original [Jetpack forums](http://wordpress.org/support/plugin/jetpack). Your chances of getting help will be much better. I'm not the developer of this plugin. See notes below.
* If, otherwise, this plugin does not work (which means it is not activating or it's breaking your bolg), please ask [here](https://wordpress.org/support/plugin/jetpack-widget-visibility), and I shall help you fix it ASAP.

= Notes =

[Jetpack](http://wordpress.org/plugins/jetpack/) is a plugin that ships with many modules. Why install the whole package, if you're just interested in one module?!

This plugin is the exact [Widget Visibility module](http://jetpack.me/support/widget-visibility/) of the original Jetpack plugin, only without all the extra stuff.

The version number of this plugin will follow the version number of Jetpack. This way, it's easier for you to know which Jetpack version this module was extracted from.

Things you'd be happy to know:

* The module is almost untouched. (Some lines had to be removed and some had to be added, for the sole reason of making the module a stand-alone plugin).
* The module was carefully extracted from the package. That means there is no missing feature or some irrelevant code.
* The module does not require a connection to a WordPress.com account.

Note: Translations were not included, due to the fact that Jetpack uses one transaltion file for all the modules, which makes it really difficult to extract translations.
The good news is that the module can be translated easily.

= You may also like =

* [Jetpack Sharing](http://wordpress.org/plugins/jetpack-sharing/) - Share content with Facebook, Twitter, and many more.
* [Jetpack Gravatar Hovercards](http://wordpress.org/plugins/jetpack-gravatar-hovercards/) - Show a pop-up business card of your users' gravatar profiles in comments.
* [Jetpack Omnisearch](http://wordpress.org/plugins/jetpack-omnisearch/) - A single search box, that lets you search many different things.
* [Jetpack Markdown](http://wordpress.org/plugins/jetpack-markdown/) - Write in Markdown, publish in HTML.

== Installation ==

1. Install Jetpack Widget Visibility either via the WordPress.org plugin directory, or by uploading the files to your server.
2. Activate Jetpack Widget Visibility through the 'Plugins' menu in WordPress.
3. That's it. You're ready to go!

== Screenshots ==

1. Visibility button
2. Visibility conditions

== Changelog ==

= 3.0.1 =

* Update to 3.0.1

= 2.9.3 =

* Update to 2.9.3

= 2.9 =

* Update to 2.9:
* Widget Visibility: Add support for old-style single use widgets.
* Bugfix

= 2.7 =

* Update to Jetpack 2.7

= 2.5 =

* Initial release
