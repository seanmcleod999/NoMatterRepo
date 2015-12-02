<?php
// HOOKS
function soundcloud_master_notice() {
	global $current_user;
	global $pagenow;
	$user_id = $current_user->ID;
/* Check that the user hasn't already clicked to ignore the message */
	if ( ! get_user_meta($user_id, 'soundcloud_master_notice') ) {
		parse_str($_SERVER['QUERY_STRING'], $params);
		if ( $pagenow == 'plugins.php' ) {
			echo '<div class="updated">';
		printf (__('<p><b>Congratulations!</b> TechGasp Advanced Plugin Installation Complete.</p>'));
		printf (__('<p>If you need help don\'t be shy, just issue a support ticket <a class="button" href="http://wordpress.techgasp.com/support/" target="_blank" title="Support">Technical Support</a> | <a class="button" href="http://wordpress.techgasp.com/soundcloud-master-documentation/" target="_blank" title="Documentation">Documentation</a> | <a class="button-primary" href="http://wordpress.org/plugins/soundcloud-master/" target="_blank" title="Rate Us *****"><b>RATE US *****</b></a> | <a class="button" href="%s" title="hide me forever!!!">Hide Notice</a></p>'), '?soundcloud_master_ignore=0');
		printf (__('<h3>Get a TechGasp Plugin totally FREE</h3>'));
		printf (__('<div class="description">That\'s right!!! you can get <a href="http://wordpress.techgasp.com/social-master/" target="_blank" title="take a look at Social Master"><b>Social Master</b></a> totally <b>FREE</b>.</div>'));
		printf (__('<div class="description">Use the <b>RATE US</b> button above and give this plugin a 5 star rating in wordpress. That easy!!, afterwards let us know.</div>'));
		printf (__('<br>'));
			echo "</div>";
		}
	}
}
add_action( 'admin_notices', 'soundcloud_master_notice' );

function soundcloud_master_ignore() {
    global $current_user;
		$user_id = $current_user->ID;
		/* If user clicks to ignore the notice, add that to their user meta */
		if ( isset($_GET['soundcloud_master_ignore']) && '0' == $_GET['soundcloud_master_ignore'] ) {
		add_user_meta($user_id, 'soundcloud_master_notice', 'true', true);
		}
}
add_action('admin_init', 'soundcloud_master_ignore');
?>