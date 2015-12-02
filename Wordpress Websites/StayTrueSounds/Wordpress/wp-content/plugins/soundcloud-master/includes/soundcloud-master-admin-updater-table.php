<?php
if(!class_exists('WP_List_Table')){
	require_once( ABSPATH . 'wp-admin/includes/class-wp-list-table.php' );
}
class soundcloud_master_admin_updater_table extends WP_List_Table {
	/**
	 * Display the rows of records in the table
	 * @return string, echo the markup of the rows
	 */
	function display() {
if (isset($_POST['update'])){
	if (isset($_POST['down_link_soundcloud'])){
		if ($new_down_link_soundcloud = $_POST['down_link_soundcloud']){
			if( is_multisite() ){
				if ($new_down_link_soundcloud == 'ngjvyqqvio1' ){
					echo '<div id="message" class="error"><p><b>WARNING</b>... Not a Valid Download Key.</p></br><p>The advanced version updater allows to automatically update your advanced plugin. Insert your download key, it can be found in your purchase email</p><p></p>Example: http://wordpress.techgasp.com/?paiddownloads_key=ngjvyqqvio1<p>Your key would be: <b>ngjvyqqvio1</b></p></div>';
				}
			update_site_option('down_link_soundcloud', $new_down_link_soundcloud);
			}
			else {
				if ($new_down_link_soundcloud == 'ngjvyqqvio1' ){
					echo '<div id="message" class="error"><p><b>ngjvyqqvio1</b> is not a Valid Download Key, it\'s only used for instructions.</p></br><p>The advanced version updater allows to automatically update your advanced plugin. Insert your download key, it can be found in your purchase email</p><p></p>Example: http://wordpress.techgasp.com/?paiddownloads_key=ngjvyqqvio1<p>Your key would be: <b>ngjvyqqvio1</b></p></div>';
				}
			update_option('down_link_soundcloud', $new_down_link_soundcloud);
			}
		}
	else{
		if (empty($new_down_link_soundcloud)){
			echo '<div id="message" class="error"><p><b>WARNING</b>... Not a Valid Download Key. </p></br><p>The advanced version updater allows to automatically update your advanced plugin. Insert your download key, it can be found in your purchase email</p><p></p>Example: http://wordpress.techgasp.com/?paiddownloads_key=ngjvyqqvio1<p>Your key would be: <b>ngjvyqqvio1</b></p></div>';
		}
	}
	}
?>
<div id="message" class="updated fade">
<p><strong><?php _e('Settings Saved!', 'soundcloud_master'); ?></strong></p>
</div>
<?php
if( is_multisite() ) {
delete_site_option( '_site_transient_update_plugins' );
}
else{
delete_option( '_site_transient_update_plugins' );
}
}
?>
<table class="widefat fixed" cellspacing="0">
	<thead>
		<tr>
			<th id="columnname" class="manage-column column-columnname" scope="col" width="387"><legend><h3><img src="<?php echo plugins_url('../images/techgasp-minilogo-16.png', __FILE__); ?>" style="float:left; height:16px; vertical-align:middle;" /><?php _e('&nbsp;Advanced Version Updater:', 'soundcloud_master'); ?></h3></legend></th>
			<th id="columnname" class="manage-column column-columnname" scope="col"></th>
		</tr>
	</thead>

	<tfoot>
		<tr>
			<th class="manage-column column-columnname" scope="col" width="387"></th>
			<th class="manage-column column-columnname" scope="col"></th>
		</tr>
	</tfoot>

	<tbody>
		<tr class="alternate" valign="top">
			<td class="column-columnname" width="387">
<p class="submit">Download Key: <input id="down_link_soundcloud" name="down_link_soundcloud" type="text" size="16" maxlength="16" value="<?php echo get_option('down_link_soundcloud'); ?>" > <input class='button-primary' type='submit' name='update' value='<?php _e("Save Settings", 'soundcloud_master'); ?>' id='submitbutton' /></p>
			</td>
			<td class="column-columnname">
<div class="description">The advanced version updater allows to automatically update your advanced plugin. Your download key can be found in your purchase email, it's the combination of numbers and letters of your original download link. </div>
<div class="description">Example: http://wordpress.techgasp.com/?paiddownloads_key=ngjvyqqvio1</div>
<div class="description">Your key would be: <b>ngjvyqqvio1</b></div>
			</td>
		</tr>
	</tbody>
</table>
<?php
	}
}