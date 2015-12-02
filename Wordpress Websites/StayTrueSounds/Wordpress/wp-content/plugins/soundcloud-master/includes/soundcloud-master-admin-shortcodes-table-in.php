<?php
if(!class_exists('WP_List_Table')){
	require_once( ABSPATH . 'wp-admin/includes/class-wp-list-table.php' );
}
class soundcloud_master_admin_shortcodes_table_in extends WP_List_Table {
	/**
	 * Display the rows of records in the table
	 * @return string, echo the markup of the rows
	 */
	function display() {
?>
<table class="widefat fixed" cellspacing="0">
	<thead>
		<tr>
			<th id="columnname" class="manage-column column-columnname" scope="col" width="600"><legend><h3><img src="<?php echo plugins_url('../images/techgasp-minilogo-16.png', __FILE__); ?>" style="float:left; height:16px; vertical-align:middle;" /><?php _e('&nbsp;Individual Shortcodes', 'soundcloud_master'); ?></h3></legend></th>
			<th id="columnname" class="manage-column column-columnname" scope="col"></th>
		</tr>
	</thead>

	<tfoot>
		<tr>
			<th class="manage-column column-columnname" scope="col" width="600"></th>
			<th class="manage-column column-columnname" scope="col"></th>
		</tr>
	</tfoot>

	<tbody>
		<tr class="alternate">
			<td class="column-columnname" width="600"><img src="<?php echo plugins_url('../images/techgasp-soundcloud-master-backend-edit.png', __FILE__); ?>" alt="<?php echo get_option('soundcloud_master_name'); ?>" align="left" width="600px" height="265px" style="padding:5px;"/></td>
			<td class="column-columnname">
<h3>Individual Shortcode</h3>
<p>Soundcloud Master uses TechGasp Wordpress Framework. The <b>Individual Shortcode</b> is easy to use and can be found when you edit a post or a page under the wordpress text editor. Once you have created your shortcode, Just insert the shortcode <b>[soundcloud-master]</b> anywhere inside your text.</p>
<h3>Individual vs Universal Shortcode</h3
<p>The Individual Shortcode allows you to have a different, customized shortcodes per page or post, you can configure it when editing a post or page. The Universal Shortcode configurable below, allows you to have the same shortcode in all pages and posts by adding <b>[soundcloud-master-un]</b> into the text.</p>

</td>
		</tr>
	</tbody>
</table>
<?php
		}
}