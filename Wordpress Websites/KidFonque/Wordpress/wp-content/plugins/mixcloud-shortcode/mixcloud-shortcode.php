<?php
/*
Plugin Name: Mixcloud Shortcode
Plugin URI: http://www.zsantana.com/mixcloud-shortcode/
Description: Mixcloud Shortcode for posts and pages. Defaut usage: [mixcloud]http://www.mixcloud.com/artist-name/long-live-set-name/[/mixcloud]. Make sure it's the track permalink (...com/artist-name/dj-set-or-live-name/) instead of "...com/player/". Optional parameters: height and width. [mixcloud height="100" width="400"]http://www.mixcloud.com/artist-name/recorded-live-somewhere/[/mixcloud]. The slash at the end is necessary.
Version: 1.2
Author: Zoro Santana <zoro@zsantana.com>
Author URI: http://www.zsantana.com/
*/

/* Checking if a class named mixcloudShortcode exists to avoid
naming collisions with other WordPress plugins.*/
if (!class_exists("mixcloudShortcode")) {

	// If it doesn't exist, create mixcloudShortcode class
	class mixcloudShortcode {

		function mixcloudShortcode() { //constructor
		}

		/** [mixcloud height="int value" width="int value"]
		The following function creates a "[mixcloud]" shortcode that supports two attributes: ["height" and "width"].
		Both attributes are optional and will take on default options [height="300" width="300"] if they are not provided.
		This shortcode handler function accepts two arguments:
		$atts, an associative array of attributes
		$content, the enclosed content (if the shortcode is used in its enclosing form)
		*/
		function createShortcode($atts, $content=null) {
			extract( shortcode_atts( array(
				'height' => '300',
				'width' => '300',
			), $atts ) );

			// Explode the url passed between withing the shortcode tags
			$pieces = explode('/', $content);
			foreach($pieces as $key => $value) {
				// Get the array index when mixcould found
				if ("www.mixcloud.com" === $value){
					break;	
				}	
			}

			// Build value for param tag 
			$value = 'http://www.mixcloud.com/'.$pieces[$key+1].'/'.$pieces[$key+2].'/';
			$value = "http://www.mixcloud.com/media/swf/player/mixcloudLoader.swf?feed=" . $value;
			
			// Build object to be returned
			$object = "<object type='application/x-shockwave-flash' height='$height' width='$width' data='$value'>";
			$object .= "<param name='movie' value='";
			$object .= $value . "'/>"; 
			$object .= "</object>";

			return $object; 
		}
	}
} //End class mixcloudShortcode

// If class mixcloudShortcode exists, create an object
if (class_exists("mixcloudShortcode")) {
	$obj_mixcloud = new mixcloudShortcode();
}

// If an instance of the $obj_mixcloud object was created, add shortcode 
if (isset($obj_mixcloud)) {
	// Adding 'mixcloud' shortcode. & is necessary because we are calling a function inside the class.
	add_shortcode('mixcloud', array(&$obj_mixcloud, 'createShortcode'));
}
?>
