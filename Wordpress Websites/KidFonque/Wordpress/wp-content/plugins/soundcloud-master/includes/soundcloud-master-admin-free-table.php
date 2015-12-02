<?php
if(!class_exists('WP_List_Table')){
	require_once( ABSPATH . 'wp-admin/includes/class-wp-list-table.php' );
}
class soundcloud_master_admin_free_table extends WP_List_Table {
	/**
	 * Display the rows of records in the table
	 * @return string, echo the markup of the rows
	 */
	function display() {
?>
<table class="widefat fixed" cellspacing="0">
	<thead>
		<tr>
			<th id="columnname" class="manage-column column-columnname" scope="col" width="500"><legend><h3><img src="<?php echo plugins_url('../images/techgasp-minilogo-16.png', __FILE__); ?>" style="float:left; height:16px; vertical-align:middle;" /><?php _e('&nbsp;Social Master', 'soundcloud_master'); ?></h3></legend></th>
			<th id="columnname" class="manage-column column-columnname" scope="col"></th>
		</tr>
	</thead>

	<tfoot>
		<tr>
			<th class="manage-column column-columnname" scope="col" width="500"></th>
			<th class="manage-column column-columnname" scope="col"></th>
		</tr>
	</tfoot>

	<tbody>
		<tr class="alternate">
			<td class="column-columnname" width="500"><img src="<?php echo plugins_url('../images/techgasp-socialmaster-logo.png', __FILE__); ?>" alt="Social Master" align="left" width="500px" style="padding:5px;"/></td>
			<td class="column-columnname">
<h3>Totally FREE</h3>
<p>That's right!!! you can get Social Master totally <b>FREE</b>.</p>
<a class="button-primary" href="http://wordpress.techgasp.com/social-master/" target="_blank" title="Visit Social Master">Social Master Info</a>
<p>Use the <b>RATE US</b> button below and give this plugin a 5 star rating in wordpress. That easy!!, afterwards let us know.</p>
<h3>Rate Us 5 *****</h3>
<a class="button-primary" href="http://wordpress.org/plugins/soundcloud-master/" target="_blank" title="Rate Us *****"><b>RATE US *****</b></a>
		</td>
		</tr>
		<tr>
			<td class="column-columnname" width="500"></td>
			<td class="column-columnname">
<p><b>Social Master</b> is a light weight and shiny clean code wordpress plugin WIDGET that you need to boost your wordpress social engagement, attracting new users, visits or sales. Social Master replaces a bunch of extensions keeping your wordpress website code clean and with fast page loading times because it makes NO use of Javascipt or Ajax.</p>
<p>Buil-in html5 and iframe, Social Master combines all major social networks sharing tools, Facebook Like and Send, Twitter Follow and Re-Tweet, Google + plus, LinkedIn Share, Tumblr Follow, Pinterest "pin it" Share, View on Instagram, Youtube Subscribe, Soundcloud Profile, Reverbnation Profile, Spotify profile, StumbleUpon Share, MySpace Share, Buffer Share, Digg Share and Reddit Share, with more social networks coming in future updates.</p>
<p>The plugin is packed with many display options, in fact you can decide what buttons to show in the frontend and also the bubble per button that displays the number of social sharing. It will give your website or blog the needed social sharing viral boost while making your content look like a professional.</p>
		</td>
		</tr>
	</tbody>
</table>
<?php
		}
}