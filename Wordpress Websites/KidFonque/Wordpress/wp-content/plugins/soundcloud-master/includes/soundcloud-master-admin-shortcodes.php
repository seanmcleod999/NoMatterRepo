<?php
if( is_multisite() ) {
	function menu_multi_soundcloud_admin_shortcodes(){
	// Create menu
	add_submenu_page( 'soundcloud-master', 'Shortcodes', 'Shortcodes', 'manage_options', 'soundcloud-master-admin-shortcodes', 'soundcloud_master_admin_shortcodes' );
	}
}
else {
	// Create menu
	function menu_single_soundcloud_admin_shortcodes(){
		if ( is_admin() )
		add_submenu_page( 'soundcloud-master', 'Shortcodes', 'Shortcodes', 'manage_options', 'soundcloud-master-admin-shortcodes', 'soundcloud_master_admin_shortcodes' );
	}
}

function soundcloud_master_admin_shortcodes(){
?>
<div class="wrap">
<div style="width:40px; vertical-align:middle; float:left;"><img src="<?php echo plugins_url('../images/techgasp-minilogo.png', __FILE__); ?>" alt="' . esc_attr__( 'TechGasp Plugins') . '" /><br /></div>
<h2><b>&nbsp;Shortcodes</b></h2>
<?php
if(!class_exists('soundcloud_master_admin_shortcodes_table_in')){
	require_once( dirname( __FILE__ ) . '/soundcloud-master-admin-shortcodes-table-in.php');
}
//Prepare Table of elements
$wp_list_table = new soundcloud_master_admin_shortcodes_table_in();
//Table of elements
$wp_list_table->display();

?>
</br>
<div style="background: url(<?php echo plugins_url('../images/techgasp-hr.png', __FILE__); ?>) repeat-x; height: 10px"></div>
</br>
<?php
if(!class_exists('soundcloud_master_admin_shortcodes_table_un')){
	require_once( dirname( __FILE__ ) . '/soundcloud-master-admin-shortcodes-table-un.php');
}
//Prepare Table of elements
$wp_list_table = new soundcloud_master_admin_shortcodes_table_un();
//Table of elements
$wp_list_table->display();
?>
</br>
<h2>IMPORTANT: Makes no use of Javascript or Ajax to keep your website fast and conflicts free</h2>

<div style="background: url(<?php echo plugins_url('../images/techgasp-hr.png', __FILE__); ?>) repeat-x; height: 10px"></div>

<br>

<p>
<a class="button-secondary" href="http://wordpress.techgasp.com" target="_blank" title="Visit Website">More TechGasp Plugins</a>
<a class="button-secondary" href="http://wordpress.techgasp.com/support/" target="_blank" title="Facebook Page">TechGasp Support</a>
<a class="button-primary" href="http://wordpress.techgasp.com/soundcloud-master/" target="_blank" title="Visit Website"><?php echo get_option('soundcloud_master_name'); ?> Info</a>
<a class="button-primary" href="http://wordpress.techgasp.com/soundcloud-master-documentation/" target="_blank" title="Visit Website"><?php echo get_option('soundcloud_master_name'); ?> Documentation</a>
<a class="button-primary" href="http://wordpress.org/plugins/soundcloud-master/" target="_blank" title="Visit Website">RATE US *****</a>
</p>
<?php
}

if( is_multisite() ) {
add_action( 'network_admin_menu', 'menu_multi_soundcloud_admin_shortcodes' );
}
else {
add_action( 'admin_menu', 'menu_single_soundcloud_admin_shortcodes' );
}
?>