<?php
/*
 * Plugin Name: Meta Slider - Pro Addon Pack
 * Plugin URI: http://www.metaslider.com
 * Description: Supercharge your slideshows!
 * Version: 2.4.2
 * Author: Matcha Labs
 * Author URI: http://www.matchalabs.com
 * Copyright: Matcha Labs LTD 2014
 */

// disable direct access
if ( ! defined( 'ABSPATH' ) ) {
    exit;
}

if ( ! class_exists( 'MetaSliderPro' ) ) :

/**
 * Register the plugin.
 *
 * Display the administration panel, insert JavaScript etc.
 */
class MetaSliderPro {

    /**
     * @var string
     */
    public $version = '2.4.2';

    /**
     * Init
     */
    public static function init() {

        $metasliderpro = new self();

    }

    /**
     * Constructor
     */
    public function __construct() {

        if ( ! class_exists( 'MetaSlide' ) ) {
            // check Meta Slider (Lite) is installed and activated
            if ( ! class_exists( 'TGM_Plugin_Activation' ) ) {
                require_once 'inc/class-tgm-plugin-activation.php';
            }

            add_action( 'tgmpa_register', array( $this, 'check_metaslider_lite_dependency' ) );

            return;
        }

        define( 'METASLIDERPRO_VERSION',    $this->version );
        define( 'METASLIDERPRO_BASE_URL',   trailingslashit( plugins_url( 'ml-slider-pro' ) ) );
        define( 'METASLIDERPRO_ASSETS_URL', trailingslashit( METASLIDERPRO_BASE_URL . 'assets' ) );
        define( 'METASLIDERPRO_PATH',       plugin_dir_path( __FILE__ ) );

        $this->includes();

        add_filter( 'metaslider_menu_title', array( $this, 'menu_title' ) );
        add_action( 'init', array( $this, 'load_plugin_textdomain' ) );
        add_action( 'metaslider_register_admin_scripts', array( $this, 'register_admin_scripts' ), 10, 1 );
        add_action( 'metaslider_register_admin_styles', array( $this, 'register_admin_styles' ), 10, 1 );
        add_filter( 'metaslider_css', array( $this, 'get_public_css' ), 11, 3 );

        new WPUpdatesPluginUpdater( 'http://wp-updates.com/api/1/plugin', 136, plugin_basename( __FILE__ ) );
        new MetaSliderThemeEditor();
        new MetaSliderThumbnails();
        new MetaPostFeedSlide();
        new MetaVimeoSlide();
        new MetaYouTubeSlide();
        new MetaLayerSlide();
        new MetaSliderLoop();
    }

    /**
     * All Meta Slider classes
     */
    private function plugin_classes() {

        return array(
            'metalayerslide'         => METASLIDERPRO_PATH . 'modules/layer/slide.php',
            'metayoutubeslide'       => METASLIDERPRO_PATH . 'modules/youtube/slide.php',
            'metavimeoslide'         => METASLIDERPRO_PATH . 'modules/vimeo/slide.php',
            'metapostfeedslide'      => METASLIDERPRO_PATH . 'modules/post_feed/slide.php',
            'metasliderthumbnails'   => METASLIDERPRO_PATH . 'modules/thumbnails/thumbnails.php',
            'metasliderthemeeditor'  => METASLIDERPRO_PATH . 'modules/theme_editor/theme_editor.php',
            'metasliderloop'         => METASLIDERPRO_PATH . 'modules/extra/loop.php',
            'wpupdatespluginupdater' => METASLIDERPRO_PATH . 'inc/wp-updates-plugin.php'
        );

    }


    /**
     * Load required classes
     */
    private function includes() {

        $autoload_is_disabled = defined( 'METASLIDER_AUTOLOAD_CLASSES' ) && METASLIDER_AUTOLOAD_CLASSES === false;

        if ( function_exists( "spl_autoload_register" ) && ! ( $autoload_is_disabled ) ) {

            // >= PHP 5.2 - Use auto loading
            if ( function_exists( "__autoload" ) ) {
                spl_autoload_register( "__autoload" );
            }

            spl_autoload_register( array( $this, 'autoload' ) );

        } else {

            // < PHP5.2 - Require all classes
            foreach ( $this->plugin_classes() as $id => $path ) {
                if ( is_readable( $path ) && ! class_exists( $id ) ) {
                    require_once( $path );
                }
            }

        }

    }


    /**
     * Autoload Meta Slider classes to reduce memory consumption
     */
    private function autoload( $class ) {

        $classes = $this->plugin_classes();

        $class_name = strtolower( $class );

        if ( isset( $classes[$class_name] ) && is_readable( $classes[$class_name] ) ) {
            require_once $classes[$class_name];
        }

    }


    /**
     * Initialise translations
     */
    public function load_plugin_textdomain() {
        load_plugin_textdomain( 'metasliderpro', false, dirname( plugin_basename( __FILE__ ) ) . '/languages/' );
    }

    /**
     * Registers and enqueues admin JavaScript
     */
    public function register_admin_scripts() {
        wp_enqueue_script( 'metaslider-pro-admin-script', METASLIDERPRO_ASSETS_URL . 'admin.js', array( 'jquery', 'metaslider-admin-script' ), METASLIDERPRO_VERSION );
    }

    /**
     * Registers and enqueues admin CSS
     */
    public function register_admin_styles() {
        wp_enqueue_style( 'metaslider-pro-admin-styles', METASLIDERPRO_ASSETS_URL . 'admin.css', false, METASLIDERPRO_VERSION );
    }

    /**
     * Registers and enqueues public CSS
     *
     * @param string  $css
     * @param array   $settings
     * @param int     $id
     * @return string
     */
    public function get_public_css( $css, $settings, $id ) {
        if ( $settings['printCss'] == 'true' ) {
            wp_enqueue_style( 'metaslider-pro-public', METASLIDERPRO_ASSETS_URL . "public.css", false, METASLIDERPRO_VERSION );
        }
    }

    /**
     * Add "Pro" to the menu title
     *
     * @param string  Meta Slider menu name
     * @return string title
     */
    public function menu_title( $title ) {
        return $title . " " . __("Pro", "metasliderpro");
    }


    /**
     * Register the required plugins for this theme.
     *
     * In this example, we register two plugins - one included with the TGMPA library
     * and one from the .org repo.
     *
     * The variable passed to tgmpa_register_plugins() should be an array of plugin
     * arrays.
     *
     * This function is hooked into tgmpa_init, which is fired within the
     * TGM_Plugin_Activation class constructor.
     */
    public function check_metaslider_lite_dependency() {

        /**
         * Array of plugin arrays. Required keys are name, slug and required.
         * If the source is NOT from the .org repo, then source is also required.
         */
        $plugins = array(
            // This is an example of how to include a plugin from the WordPress Plugin Repository
            array(
                'name'      => 'Meta Slider',
                'slug'      => 'ml-slider',
                'required'  => true,
                'version'   => 2.8
            ),
        );

        // Change this to your theme text domain, used for internationalising strings
        $theme_text_domain = 'metasliderpro';

        /**
         * Array of configuration settings. Amend each line as needed.
         * If you want the default strings to be available under your own theme domain,
         * leave the strings uncommented.
         * Some of the strings are added into a sprintf, so see the comments at the
         * end of each line for what each argument will be.
         */
        $config = array(
            'domain'            => $theme_text_domain,           // Text domain - likely want to be the same as your theme.
            'default_path'      => '',                           // Default absolute path to pre-packaged plugins
            'parent_menu_slug'  => 'themes.php',         // Default parent menu slug
            'parent_url_slug'   => 'themes.php',         // Default parent URL slug
            'menu'              => 'install-required-plugins',   // Menu slug
            'has_notices'       => true,                         // Show admin notices or not
            'is_automatic'      => true,            // Automatically activate plugins after installation or not
            'message'           => '',               // Message to output right before the plugins table
            'strings'           => array(
                'page_title'                                => __( 'Install Required Plugins', $theme_text_domain ),
                'menu_title'                                => __( 'Install Plugins', $theme_text_domain ),
                'installing'                                => __( 'Installing Plugin: %s', $theme_text_domain ), // %1$s = plugin name
                'oops'                                      => __( 'Something went wrong with the plugin API.', $theme_text_domain ),
                'notice_can_install_required'               => _n_noop( 'Meta Slider (Pro) requires the following plugin: %1$s.', 'This theme requires the following plugins: %1$s.' ), // %1$s = plugin name(s)
                'notice_can_install_recommended'            => _n_noop( 'This theme recommends the following plugin: %1$s.', 'This theme recommends the following plugins: %1$s.' ), // %1$s = plugin name(s)
                'notice_cannot_install'                     => _n_noop( 'Sorry, but you do not have the correct permissions to install the %s plugin. Contact the administrator of this site for help on getting the plugin installed.', 'Sorry, but you do not have the correct permissions to install the %s plugins. Contact the administrator of this site for help on getting the plugins installed.' ), // %1$s = plugin name(s)
                'notice_can_activate_required'              => _n_noop( 'The following required plugin is currently inactive: %1$s.', 'The following required plugins are currently inactive: %1$s.' ), // %1$s = plugin name(s)
                'notice_can_activate_recommended'           => _n_noop( 'The following recommended plugin is currently inactive: %1$s.', 'The following recommended plugins are currently inactive: %1$s.' ), // %1$s = plugin name(s)
                'notice_cannot_activate'                    => _n_noop( 'Sorry, but you do not have the correct permissions to activate the %s plugin. Contact the administrator of this site for help on getting the plugin activated.', 'Sorry, but you do not have the correct permissions to activate the %s plugins. Contact the administrator of this site for help on getting the plugins activated.' ), // %1$s = plugin name(s)
                'notice_ask_to_update'                      => _n_noop( 'The following plugin needs to be updated to its latest version to ensure maximum compatibility with this theme: %1$s.', 'The following plugins need to be updated to their latest version to ensure maximum compatibility with this theme: %1$s.' ), // %1$s = plugin name(s)
                'notice_cannot_update'                      => _n_noop( 'Sorry, but you do not have the correct permissions to update the %s plugin. Contact the administrator of this site for help on getting the plugin updated.', 'Sorry, but you do not have the correct permissions to update the %s plugins. Contact the administrator of this site for help on getting the plugins updated.' ), // %1$s = plugin name(s)
                'install_link'                              => _n_noop( 'Begin installing plugin', 'Begin installing plugins' ),
                'activate_link'                             => _n_noop( 'Activate installed plugin', 'Activate installed plugins' ),
                'return'                                    => __( 'Return to Required Plugins Installer', $theme_text_domain ),
                'plugin_activated'                          => __( 'Plugin activated successfully.', $theme_text_domain ),
                'complete'                                  => __( 'All plugins installed and activated successfully. %s', $theme_text_domain ) // %1$s = dashboard link
            )
        );

        tgmpa( $plugins, $config );

    }

}

endif;

add_action( 'plugins_loaded', array( 'MetaSliderPro', 'init' ), 11 );
