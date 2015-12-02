<?php
if(!class_exists('WP_List_Table')){
	require_once( ABSPATH . 'wp-admin/includes/class-wp-list-table.php' );
}
class soundcloud_master_admin_shortcodes_table_un extends WP_List_Table {
	/**
	 * Display the rows of records in the table
	 * @return string, echo the markup of the rows
	 */
	function display() {
if ( $_POST) {
if ( isset($_POST['soundcloud_master_un_auto']) )
update_option('soundcloud_master_un_auto', $_POST['soundcloud_master_un_auto'] );
else
update_option('soundcloud_master_un_auto', 'false' );

if ( isset($_POST['soundcloud_master_un_auto_top']) )
update_option('soundcloud_master_un_auto_top', $_POST['soundcloud_master_un_auto_top'] );
else
update_option('soundcloud_master_un_auto_top', 'false' );

if ( isset($_POST['soundcloud_master_un_auto_posts']) )
update_option('soundcloud_master_un_auto_posts', $_POST['soundcloud_master_un_auto_posts'] );
else
update_option('soundcloud_master_un_auto_posts', 'false' );

if ( isset($_POST['soundcloud_master_un_button_connect']) )
update_option('soundcloud_master_un_button_connect', $_POST['soundcloud_master_un_button_connect'] );
else
update_option('soundcloud_master_un_button_connect', 'false' );

if ( isset($_POST['soundcloud_master_un_button_page_connect']) )
update_option('soundcloud_master_un_button_page_connect', $_POST['soundcloud_master_un_button_page_connect'] );
else
update_option('soundcloud_master_un_button_page_connect', 'false' );

if ( isset($_POST['soundcloud_master_un_button_lyrics']) )
update_option('soundcloud_master_un_button_lyrics', $_POST['soundcloud_master_un_button_lyrics'] );
else
update_option('soundcloud_master_un_button_lyrics', 'false' );

if ( isset($_POST['soundcloud_master_un_button_page_lyrics']) )
update_option('soundcloud_master_un_button_page_lyrics', $_POST['soundcloud_master_un_button_page_lyrics'] );
else
update_option('soundcloud_master_un_button_page_lyrics', 'false' );

if ( isset($_POST['soundcloud_master_un_player']) )
update_option('soundcloud_master_un_player', $_POST['soundcloud_master_un_player'] );
else
update_option('soundcloud_master_un_player', 'false' );

if ( isset($_POST['soundcloud_master_un_player_page']) )
update_option('soundcloud_master_un_player_page', $_POST['soundcloud_master_un_player_page'] );
else
update_option('soundcloud_master_un_player_page', 'false' );

if ( isset($_POST['soundcloud_master_un_player_height']) )
update_option('soundcloud_master_un_player_height', $_POST['soundcloud_master_un_player_height'] );
else
update_option('soundcloud_master_un_player_height', '100%' );

if ( isset($_POST['soundcloud_master_un_player_autoplay']) )
update_option('soundcloud_master_un_player_autoplay', $_POST['soundcloud_master_un_player_autoplay'] );
else
update_option('soundcloud_master_un_player_autoplay', 'false' );

if ( isset($_POST['soundcloud_master_un_player_showuser']) )
update_option('soundcloud_master_un_player_showuser', $_POST['soundcloud_master_un_player_showuser'] );
else
update_option('soundcloud_master_un_player_showuser', 'false' );

if ( isset($_POST['soundcloud_master_un_player_artwork']) )
update_option('soundcloud_master_un_player_artwork', $_POST['soundcloud_master_un_player_artwork'] );
else
update_option('soundcloud_master_un_player_artwork', 'false' );

if ( isset($_POST['soundcloud_master_un_player_comments']) )
update_option('soundcloud_master_un_player_comments', $_POST['soundcloud_master_un_player_comments'] );
else
update_option('soundcloud_master_un_player_comments', 'false' );

if ( isset($_POST['soundcloud_master_un_player_playcount']) )
update_option('soundcloud_master_un_player_playcount', $_POST['soundcloud_master_un_player_playcount'] );
else
update_option('soundcloud_master_un_player_playcount', 'false' );

if ( isset($_POST['soundcloud_master_un_player_sharing']) )
update_option('soundcloud_master_un_player_sharing', $_POST['soundcloud_master_un_player_sharing'] );
else
update_option('soundcloud_master_un_player_sharing', 'false' );

if ( isset($_POST['soundcloud_master_un_player_liking']) )
update_option('soundcloud_master_un_player_liking', $_POST['soundcloud_master_un_player_liking'] );
else
update_option('soundcloud_master_un_player_liking', 'false' );

if ( isset($_POST['soundcloud_master_un_player_download']) )
update_option('soundcloud_master_un_player_download', $_POST['soundcloud_master_un_player_download'] );
else
update_option('soundcloud_master_un_player_download', 'false' );

if ( isset($_POST['soundcloud_master_un_player_buying']) )
update_option('soundcloud_master_un_player_buying', $_POST['soundcloud_master_un_player_buying'] );
else
update_option('soundcloud_master_un_player_buying', 'false' );

if ( isset($_POST['soundcloud_master_un_player_bang']) )
update_option('soundcloud_master_un_player_bang', $_POST['soundcloud_master_un_player_bang'] );
else
update_option('soundcloud_master_un_player_bang', 'false' );
?>
<div id="message" class="updated fade">
<p><strong><?php _e('Settings Saved!', 'soundcloud_master'); ?></strong></p>
</div>
<?php
}
?>
<form method="post" width='1'>
<fieldset class="options">

<table class="widefat fixed" cellspacing="0">
	<thead>
		<tr>
			<th id="cb" class="manage-column column-cb check-column" scope="col" style="vertical-align:middle"><input type="checkbox"></th>
			<th id="columnname" class="manage-column column-columnname" scope="col" width="200"><legend><h3><img src="<?php echo plugins_url('../images/techgasp-minilogo-16.png', __FILE__); ?>" style="float:left; height:16px; vertical-align:middle;" /><?php _e('&nbsp;Universal Shortcode', 'soundcloud_master'); ?></h3></legend></th>
			<th id="columnname" class="manage-column column-columnname" scope="col" width="200"></th>
			<th id="columnname" class="manage-column column-columnname" scope="col"><legend><h3><?php _e('&nbsp;[soundcloud-master-un]', 'soundcloud_master'); ?></h3></legend></th>
		</tr>
	</thead>

	<tfoot>
		<tr>
			<th class="manage-column column-cb check-column" scope="col"></th>
			<th class="manage-column column-columnname" scope="col" width="200"></th>
			<th class="manage-column column-columnname" scope="col" width="200"></th>
			<th class="manage-column column-columnname" scope="col"></th>
		</tr>
	</tfoot>

	<tbody>
		<tr class="alternate">
			<th class="check-column" scope="row">
<input name="soundcloud_master_un_auto" id="soundcloud_master_un_auto" value="true" type="checkbox" <?php echo get_option('soundcloud_master_un_auto') == 'true' ? 'checked="checked"':''; ?> />
			</th>
			<td class="column-columnname" width="220">
<label for="soundcloud_master_un_auto"><b><?php _e('Activate Automatic Shortcode', 'soundcloud_master'); ?></b></label>
			</td>
			<td class="column-columnname" width="250"></td>
			<td class="column-columnname" style="vertical-align:middle">Automatically adds the Universal Shortcode to All your Pages and Posts. If off, you need to manually add the shortcode [soundcloud-master-un] inside your pages and posts text.</td>
		</tr>
		<tr class="alternate">
			<th class="check-column" scope="row">
<input name="soundcloud_master_un_auto_posts" id="soundcloud_master_un_auto_posts" value="true" type="checkbox" <?php echo get_option('soundcloud_master_un_auto_posts') == 'true' ? 'checked="checked"':''; ?> />
			</th>
			<td class="column-columnname" width="220">
<label for="soundcloud_master_un_auto_posts"><b><?php _e('Activate Shortcode in Posts Only', 'soundcloud_master'); ?></b></label>
			</td>
			<td class="column-columnname" width="250"></td>
			<td class="column-columnname" style="vertical-align:middle"><b>Automatic Shortcode must be Activated</b>. Adds Shortcode to Posts only. If off, Shortode is added to both <b>Posts</b> and <b>Pages</b>. Default is off.</td>
		</tr>
		<tr class="alternate">
			<th class="check-column" scope="row">
<input name="soundcloud_master_un_auto_top" id="soundcloud_master_un_auto_top" value="true" type="checkbox" <?php echo get_option('soundcloud_master_un_auto_top') == 'true' ? 'checked="checked"':''; ?> />
			</th>
			<td class="column-columnname" width="220">
<label for="soundcloud_master_un_auto_top"><b><?php _e('Activate Shortcode After Title', 'soundcloud_master'); ?></b></label>
			</td>
			<td class="column-columnname" width="250"></td>
			<td class="column-columnname"><b>Automatic Shortcode must be Activated</b>. Adds Shortcode at the Top of the Page or Post, after the Title. If off, Shortode is generated at the Bottom of the Page or Post.</td>
		</tr>
		<tr>
			<th class="check-column" scope="row">
<input name="soundcloud_master_un_button_connect" id="soundcloud_master_un_button_connect" value="true" type="checkbox" <?php echo get_option('soundcloud_master_un_button_connect') == 'true' ? 'checked="checked"':''; ?> />
			</th>
			<td class="column-columnname" width="200">
<label for="soundcloud_master_un_button_connect"><b><?php _e('Soundcloud Connect Button', 'soundcloud_master'); ?></b></label>
			</td>
			<td class="column-columnname" width="200"></td>
			<td class="column-columnname"></td>
		</tr>
		<tr>
			<th class="check-column" scope="row"></th>
			<td class="column-columnname" width="200">
<label for="soundcloud_master_un_button_page_lyrics"><?php _e('Soundcloud Profile Link:', 'soundcloud_master'); ?></label>
			</td>
			<td class="column-columnname" width="200">
<input id="soundcloud_master_un_button_page_connect" name="soundcloud_master_un_button_page_connect" type="text" size="22" value="<?php echo get_option('soundcloud_master_un_button_page_connect'); ?>">
			</td>
			<td class="column-columnname">
<p>Insert your Soundcloud Profile Page link</p>
			</td>
		</tr>
		<tr class="alternate">
			<th class="check-column" scope="row">
<input name="soundcloud_master_un_button_lyrics" id="soundcloud_master_un_button_lyrics" value="true" type="checkbox" <?php echo get_option('soundcloud_master_un_button_lyrics') == 'true' ? 'checked="checked"':''; ?> />
			</th>
			<td class="column-columnname" width="200">
<label for="soundcloud_master_un_button_lyrics"><b><?php _e('Soundcloud Lyrics Button', 'soundcloud_master'); ?></b></label>
			</td>
			<td class="column-columnname" width="200"></td>
			<td class="column-columnname"></td>
		</tr>
		<tr class="alternate">
			<th class="check-column" scope="row"></th>
			<td class="column-columnname" width="200">
<label for="soundcloud_master_un_button_page_lyrics"><?php _e('Soundcloud Lyrics Link:', 'soundcloud_master'); ?></label>
			</td>
			<td class="column-columnname" width="200">
<input id="soundcloud_master_un_button_page_lyrics" name="soundcloud_master_un_button_page_lyrics" type="text" size="22" value="<?php echo get_option('soundcloud_master_un_button_page_lyrics'); ?>">
			</td>
			<td class="column-columnname">
<p>Insert your Lyrics Page Link</p>
			</td>
		</tr>
		<tr>
			<th class="check-column" scope="row">
<input name="soundcloud_master_un_player" id="soundcloud_master_un_player" value="true" type="checkbox" <?php echo get_option('soundcloud_master_un_player') == 'true' ? 'checked="checked"':''; ?> />
			</th>
			<td class="column-columnname" width="200">
<label for="soundcloud_master_un_player"><b><?php _e('Show Soundcloud Player', 'soundcloud_master'); ?></b></label>
			</td>
			<td class="column-columnname" width="200"></td>
			<td class="column-columnname"></td>
		</tr>
		<tr>
			<th class="check-column" scope="row"></th>
			<td class="column-columnname" width="200">
<label for="soundcloud_master_un_player_page"><?php _e('Track or Set of Tracks Link:', 'soundcloud_master'); ?></label>
			</td>
			<td class="column-columnname" width="200">
<input id="soundcloud_master_un_player_page" name="soundcloud_master_un_player_page" type="text" size="22" value="<?php echo get_option('soundcloud_master_un_player_page'); ?>">
			</td>
			<td class="column-columnname">
<p>Copy and Paste from your browser the link from a single track or set of tracks</p>
			</td>
		</tr>
		<tr>
			<th class="check-column" scope="row"></th>
			<td class="column-columnname" width="200">
<label for="soundcloud_master_un_player_height"><?php _e('Soundcloud Player Height:', 'soundcloud_master'); ?></label>
			</td>
			<td class="column-columnname" width="200">
<input id="soundcloud_master_un_player_height" name="soundcloud_master_un_player_height" type="text" size="22" value="<?php echo get_option('soundcloud_master_un_player_height'); ?>">
			</td>
			<td class="column-columnname">
<p>Default is height is between <b>160 and 450</b> . You can play with this value, it will NOT affect mobile responsiveness.</p>
			</td>
		</tr>
		<tr>
			<th class="check-column" scope="row">
<input name="soundcloud_master_un_player_autoplay" id="soundcloud_master_un_player_autoplay" value="true" type="checkbox" <?php echo get_option('soundcloud_master_un_player_autoplay') == 'true' ? 'checked="checked"':''; ?> />
			</th>
			<td class="column-columnname" width="200">
<label for="soundcloud_master_un_player_autoplay"><b><?php _e('Auto-Play', 'soundcloud_master'); ?></b></label>
			</td>
			<td class="column-columnname" width="200"></td>
			<td class="column-columnname"></td>
		</tr>
		<tr>
			<th class="check-column" scope="row">
<input name="soundcloud_master_un_player_showuser" id="soundcloud_master_un_player_showuser" value="true" type="checkbox" <?php echo get_option('soundcloud_master_un_player_showuser') == 'true' ? 'checked="checked"':''; ?> />
			</th>
			<td class="column-columnname" width="200">
<label for="soundcloud_master_un_player_showuser"><b><?php _e('Display User', 'soundcloud_master'); ?></b></label>
			</td>
			<td class="column-columnname" width="200"></td>
			<td class="column-columnname"></td>
		</tr>
		<tr>
			<th class="check-column" scope="row">
<input name="soundcloud_master_un_player_artwork" id="soundcloud_master_un_player_artwork" value="true" type="checkbox" <?php echo get_option('soundcloud_master_un_player_artwork') == 'true' ? 'checked="checked"':''; ?> />
			</th>
			<td class="column-columnname" width="200">
<label for="soundcloud_master_un_player_artwork"><b><?php _e('Display Artwork', 'soundcloud_master'); ?></b></label>
			</td>
			<td class="column-columnname" width="200"></td>
			<td class="column-columnname"></td>
		</tr>
		<tr>
			<th class="check-column" scope="row">
<input name="soundcloud_master_un_player_comments" id="soundcloud_master_un_player_comments" value="true" type="checkbox" <?php echo get_option('soundcloud_master_un_player_comments') == 'true' ? 'checked="checked"':''; ?> />
			</th>
			<td class="column-columnname" width="200">
<label for="soundcloud_master_un_player_comments"><b><?php _e('Display Comments', 'soundcloud_master'); ?></b></label>
			</td>
			<td class="column-columnname" width="200"></td>
			<td class="column-columnname"></td>
		</tr>
		<tr>
			<th class="check-column" scope="row">
<input name="soundcloud_master_un_player_playcount" id="soundcloud_master_un_player_playcount" value="true" type="checkbox" <?php echo get_option('soundcloud_master_un_player_playcount') == 'true' ? 'checked="checked"':''; ?> />
			</th>
			<td class="column-columnname" width="200">
<label for="soundcloud_master_un_player_playcount"><b><?php _e('Display Playcount', 'soundcloud_master'); ?></b></label>
			</td>
			<td class="column-columnname" width="200"></td>
			<td class="column-columnname"></td>
		</tr>
		<tr>
			<th class="check-column" scope="row">
<input name="soundcloud_master_un_player_sharing" id="soundcloud_master_un_player_sharing" value="true" type="checkbox" <?php echo get_option('soundcloud_master_un_player_sharing') == 'true' ? 'checked="checked"':''; ?> />
			</th>
			<td class="column-columnname" width="200">
<label for="soundcloud_master_un_player_sharing"><b><?php _e('Display Share', 'soundcloud_master'); ?></b></label>
			</td>
			<td class="column-columnname" width="200"></td>
			<td class="column-columnname"></td>
		</tr>
		<tr>
			<th class="check-column" scope="row">
<input name="soundcloud_master_un_player_liking" id="soundcloud_master_un_player_liking" value="true" type="checkbox" <?php echo get_option('soundcloud_master_un_player_liking') == 'true' ? 'checked="checked"':''; ?> />
			</th>
			<td class="column-columnname" width="200">
<label for="soundcloud_master_un_player_liking"><b><?php _e('Display Like', 'soundcloud_master'); ?></b></label>
			</td>
			<td class="column-columnname" width="200"></td>
			<td class="column-columnname"></td>
		</tr>
		<tr>
			<th class="check-column" scope="row">
<input name="soundcloud_master_un_player_download" id="soundcloud_master_un_player_download" value="true" type="checkbox" <?php echo get_option('soundcloud_master_un_player_download') == 'true' ? 'checked="checked"':''; ?> />
			</th>
			<td class="column-columnname" width="200">
<label for="soundcloud_master_un_player_download"><b><?php _e('Display Download', 'soundcloud_master'); ?></b></label>
			</td>
			<td class="column-columnname" width="200"></td>
			<td class="column-columnname"></td>
		</tr>
		<tr>
			<th class="check-column" scope="row">
<input name="soundcloud_master_un_player_buying" id="soundcloud_master_un_player_buying" value="true" type="checkbox" <?php echo get_option('soundcloud_master_un_player_buying') == 'true' ? 'checked="checked"':''; ?> />
			</th>
			<td class="column-columnname" width="200">
<label for="soundcloud_master_un_player_buying"><b><?php _e('Display Buy', 'soundcloud_master'); ?></b></label>
			</td>
			<td class="column-columnname" width="200"></td>
			<td class="column-columnname"></td>
		</tr>
		<tr>
			<th class="check-column" scope="row">
<input name="soundcloud_master_un_player_bang" id="soundcloud_master_un_player_bang" value="true" type="checkbox" <?php echo get_option('soundcloud_master_un_player_bang') == 'true' ? 'checked="checked"':''; ?> />
			</th>
			<td class="column-columnname" width="200">
<label for="soundcloud_master_un_player_bang"><b><?php _e('TechGasp BANG! Artwork', 'soundcloud_master'); ?></b></label>
			</td>
			<td class="column-columnname" width="200"></td>
			<td class="column-columnname">Activate to test, for users with great Cover Artworks. Some of the above player options will be disabled.</td>
		</tr>
	</tbody>
</table>
<p class="submit"><input class='button-primary' type='submit' name='update' value='<?php _e("Save Shortcode UN", 'soundcloud_master'); ?>' id='submitbutton' /></p>
</fieldset>
</form>
<?php
	}
//CLASS ENDS
}