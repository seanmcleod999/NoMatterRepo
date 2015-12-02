<?php
/*
Plugin Name: SM's Recent Tweets Widget
Plugin URI: http://redorange.co.za
Description: This is a recent tweets widget plugin that works with Twitter API v1.1 and has a Cache.
Version: 0.1
Author: Sean Mcleod
Author URI: http://redorange.co.za
*/

/*  Copyright 2012-2014  Sean Mcleod (email : mcleod.sean@gmail.com)

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

// REGISTER STYLESHEET

function sm_recent_tweets_load_stylesheet() {
    $url = plugins_url('/css/sm-recent-tweets.css', __FILE__);
    wp_register_style('sm_recent_tweets_css', $url);
    wp_enqueue_style( 'sm_recent_tweets_css');
}
add_action('wp_print_styles', 'sm_recent_tweets_load_stylesheet');

//END REGISTER STYLESHEET

class SMRecentTweetsWidget extends WP_Widget
{
    function SMRecentTweetsWidget(){
		$widget_ops = array('description' => 'Displays Your Twitter Updates');
		$control_ops = array('width' => 300, 'height' => 300);
		parent::WP_Widget(false,$name='SM Twitter Updates Widget',$widget_ops,$control_ops);
    }

  /* Displays the Widget in the front-end */
  
    function widget($args, $instance){
		extract($args);
		$title = apply_filters('widget_title', empty($instance['title']) ? '' : $instance['title']);
		$TwitterID = empty($instance['TwitterID']) ? '' : $instance['TwitterID'];
		$TwitterCount = empty($instance['TwitterCount']) ? '' : $instance['TwitterCount'];
		$ConsumerKey = empty($instance['ConsumerKey']) ? '' : $instance['ConsumerKey'];
		$ConsumerSecret = empty($instance['ConsumerSecret']) ? '' : $instance['ConsumerSecret'];
		$AccessToken = empty($instance['AccessToken']) ? '' : $instance['AccessToken'];
		$AccessSecret = empty($instance['AccessSecret']) ? '' : $instance['AccessSecret'];
		$CacheTime = empty($instance['CacheTime']) ? '' : $instance['CacheTime'];

		echo $before_widget;

		if ( $title )
		echo $before_title . $title . $after_title;
?>



<?php
//check settings and die if not set
	if(empty($instance['ConsumerKey']) || empty($instance['ConsumerSecret']) || empty($instance['AccessToken']) || empty($instance['AccessSecret']) || empty($instance['CacheTime']) || empty($instance['TwitterID'])){
		echo '<strong>Please fill all widget settings!</strong>' . $after_widget;
		return;
	}

					
//check if cache needs update
	$sm_tweets_last_cache_time = get_option('sm_tweets_last_cache_time');
	$diff = time() - $sm_tweets_last_cache_time;
	$crt = $instance['CacheTime'] * 3600;
	
 //	yes, it needs update			
	if($diff >= $crt || empty($sm_tweets_last_cache_time)){
		
		if(!require_once('twitteroauth.php')){ 
			echo '<strong>Couldn\'t find twitteroauth.php!</strong>' . $after_widget;
			return;
		}
									
		function getConnectionWithAccessToken($cons_key, $cons_secret, $oauth_token, $oauth_token_secret) {
		  $connection = new TwitterOAuth($cons_key, $cons_secret, $oauth_token, $oauth_token_secret);
		  return $connection;
		}
		  
									  
		$connection = getConnectionWithAccessToken($instance['ConsumerKey'], $instance['ConsumerSecret'], $instance['AccessToken'], $instance['AccessSecret']);
		$tweets = $connection->get("https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name=".$instance['TwitterID']."&count=10") or die('Couldn\'t retrieve tweets! Wrong username?');
		
									
		if(!empty($tweets->errors)){
			if($tweets->errors[0]->message == 'Invalid or expired token'){
				echo '<strong>'.$tweets->errors[0]->message.'!</strong><br />You\'ll need to regenerate it <a href="https://dev.twitter.com/apps" target="_blank">here</a>!' . $after_widget;
			}else{
				echo '<strong>'.$tweets->errors[0]->message.'</strong>' . $after_widget;
			}
			return;
		}
		
		$tweets_array = array();
		for($i = 0;$i <= count($tweets); $i++){
			if(!empty($tweets[$i])){
				$tweets_array[$i]['created_at'] = $tweets[$i]->created_at;
				
					//clean tweet text
					$tweets_array[$i]['text'] = preg_replace('/[\x{10000}-\x{10FFFF}]/u', '', $tweets[$i]->text);
				
				if(!empty($tweets[$i]->id_str)){
					$tweets_array[$i]['status_id'] = $tweets[$i]->id_str;			
				}
			}	
		}							
		
		//save tweets to wp option 		
			update_option('sm_tweets_plugin',serialize($tweets_array));							
			update_option('sm_tweets_last_cache_time',time());
			
		echo '<!-- twitter cache has been updated! -->';
	}
	
	
							

$sm_tweets_plugin = maybe_unserialize(get_option('sm_tweets_plugin'));
if(!empty($sm_tweets_plugin)){
	print '<div class="clearfix">';
    print '
		<ul class="smtweets">';
		$fctr = '1';
		foreach($sm_tweets_plugin as $tweet){					
			if(!empty($tweet['text'])){
				if(empty($tweet['status_id'])){ $tweet['status_id'] = ''; }
				if(empty($tweet['created_at'])){ $tweet['created_at'] = ''; }
			
				print '<li><span class="tweetheader">Kid Fonque <a href="http://twitter.com/kidfonque">@kidfonque</a></span><br>'.sm_recent_tweets_convert_links($tweet['text']).'<br /></li>';
				if($fctr == $instance['TwitterCount']){ break; }
				$fctr++;
			}
		}
	
	print '
		</ul>';

        print '<a href="http://twitter.com/kidfonque" class=button>follow me</a>';
        print '</div>';
}

?>
	

<?php
		echo $after_widget;
	}

	/*Saves the settings. */
    function update($new_instance, $old_instance){
		$instance = $old_instance;
		$instance['title'] = stripslashes($new_instance['title']);
		$instance['TwitterCount'] = stripslashes($new_instance['TwitterCount']);
		$instance['TwitterID'] = stripslashes($new_instance['TwitterID']);
		$instance['ConsumerKey'] = stripslashes($new_instance['ConsumerKey']);
		$instance['ConsumerSecret'] = stripslashes($new_instance['ConsumerSecret']);
		$instance['AccessToken'] = stripslashes($new_instance['AccessToken']);
		$instance['AccessSecret'] = stripslashes($new_instance['AccessSecret']);
		$instance['CacheTime'] = stripslashes($new_instance['CacheTime']);

		return $instance;
	}

	/*Creates the form for the widget in the back-end. */
    function form($instance){
		//Defaults
		$instance = wp_parse_args( (array) $instance, array('title'=>'Twitter Updates', 'TwitterCount'=>'', 'TwitterID'=>'', 'ConsumerKey'=>'', 'ConsumerSecret'=>'', 'AccessToken'=>'', 'AccessSecret'=>'', 'CacheTime'=>'') );

		$title = htmlspecialchars($instance['title']);
		$TwitterCount = htmlspecialchars($instance['TwitterCount']);
		$TwitterID = htmlspecialchars($instance['TwitterID']);
		$ConsumerKey = htmlspecialchars($instance['ConsumerKey']);
		$ConsumerSecret = htmlspecialchars($instance['ConsumerSecret']);
		$AccessToken = htmlspecialchars($instance['AccessToken']);
		$AccessSecret = htmlspecialchars($instance['AccessSecret']);
		$CacheTime = htmlspecialchars($instance['CacheTime']);

		# Title
		echo '<p><label for="' . $this->get_field_id('title') . '">' . 'Title:' . '</label><input class="widefat" id="' . $this->get_field_id('title') . '" name="' . $this->get_field_name('title') . '" type="text" value="' . $title . '" /></p>';
		# Consumer Key
		echo '<p><label for="' . $this->get_field_id('ConsumerKey') . '">' . 'Consumer Key:' . '</label><input class="widefat" id="' . $this->get_field_id('ConsumerKey') . '" name="' . $this->get_field_name('ConsumerKey') . '" type="text" value="' . $ConsumerKey . '" /></p>';
		# Consumer Secret
		echo '<p><label for="' . $this->get_field_id('ConsumerSecret') . '">' . 'Consumer Secret:' . '</label><input class="widefat" id="' . $this->get_field_id('ConsumerSecret') . '" name="' . $this->get_field_name('ConsumerSecret') . '" type="text" value="' . $ConsumerSecret . '" /></p>';
		# Access Token
		echo '<p><label for="' . $this->get_field_id('AccessToken') . '">' . 'Access Token:' . '</label><input class="widefat" id="' . $this->get_field_id('AccessToken') . '" name="' . $this->get_field_name('AccessToken') . '" type="text" value="' . $AccessToken . '" /></p>';
		# Access Secret
		echo '<p><label for="' . $this->get_field_id('AccessSecret') . '">' . 'Access Secret:' . '</label><input class="widefat" id="' . $this->get_field_id('AccessSecret') . '" name="' . $this->get_field_name('AccessSecret') . '" type="text" value="' . $AccessSecret . '" /></p>';
		# Cache Time
		echo '<p><label for="' . $this->get_field_id('CacheTime') . '">' . 'Cache Tweets every: ' . '</label><input class="small-text" id="' . $this->get_field_id('CacheTime') . '" name="' . $this->get_field_name('CacheTime') . '" type="text" value="' . $CacheTime . '" /> hours</p>';
		# Twitter ID
		echo '<p><label for="' . $this->get_field_id('TwitterID') . '">' . 'Twitter Username (yourname only):' . '</label><input class="widefat" id="' . $this->get_field_id('TwitterID') . '" name="' . $this->get_field_name('TwitterID') . '" type="text" value="' . $TwitterID . '" /></p>';
		# Twitter Update Count
		echo '<p><label for="' . $this->get_field_id('TwitterCount') . '">' . 'Tweets to Show: ' . '</label>';
		echo '<select type="text" name="'.$this->get_field_name( 'TwitterCount' ).'" id="'.$this->get_field_id( 'TwitterCount' ).'">';
					$i = 1;
					for(i; $i <= 10; $i++){
						echo '<option value="'.$i.'"'; if($instance['TwitterCount'] == $i){ echo ' selected="selected"'; } echo '>'.$i.'</option>';						
					}
		echo '</select></p>';
	}

}// end SMRecentTweetsWidget class

					//convert links to clickable format
					if (!function_exists('sm_recent_tweets_convert_links')) {
						function sm_recent_tweets_convert_links($status,$targetBlank=true,$linkMaxLen=250){
						 
							// the target
								$target=$targetBlank ? " target=\"_blank\" " : "";
							 
							// convert link to url
								$status = preg_replace("/((http:\/\/|https:\/\/)[^ )
]+)/e", "'<a href=\"$1\" title=\"$1\" $target >'. ((strlen('$1')>=$linkMaxLen ? substr('$1',0,$linkMaxLen).'...':'$1')).'</a>'", $status);
							 
							// convert @ to follow
								$status = preg_replace("/(@([_a-z0-9\-]+))/i","<a href=\"http://twitter.com/$2\" title=\"Follow $2\" $target >$1</a>",$status);
							 
							// convert # to search
								$status = preg_replace("/(#([_a-z0-9\-]+))/i","<a href=\"https://twitter.com/search?q=$2\" title=\"Search $1\" $target >$1</a>",$status);
							 
							// return the status
								return $status;
						}
					}
					
					
					//convert dates to readable format	
					if (!function_exists('sm_recent_tweets_relative_time')) {
						function sm_recent_tweets_relative_time($a) {
							//get current timestampt
							$b = strtotime("now"); 
							//get timestamp when tweet created
							$c = strtotime($a);
							//get difference
							$d = $b - $c;
							//calculate different time values
							$minute = 60;
							$hour = $minute * 60;
							$day = $hour * 24;
							$week = $day * 7;
								
							if(is_numeric($d) && $d > 0) {
								//if less then 3 seconds
								if($d < 3) return "right now";
								//if less then minute
								if($d < $minute) return floor($d) . " seconds ago";
								//if less then 2 minutes
								if($d < $minute * 2) return "about 1 minute ago";
								//if less then hour
								if($d < $hour) return floor($d / $minute) . " minutes ago";
								//if less then 2 hours
								if($d < $hour * 2) return "about 1 hour ago";
								//if less then day
								if($d < $day) return floor($d / $hour) . " hours ago";
								//if more then day, but less then 2 days
								if($d > $day && $d < $day * 2) return "yesterday";
								//if less then year
								if($d < $day * 365) return floor($d / $day) . " days ago";
								//else return more than a year
								return "over a year ago";
							}
						}	
					}	


function sm_recent_tweets_WidgetInit() {
  register_widget('SMRecentTweetsWidget');
}

add_action('widgets_init', 'sm_recent_tweets_WidgetInit');

?>