<?php

// disable direct access
if ( ! defined( 'ABSPATH' ) ) exit;

/**
 * Vimeo Slide
 */
class MetaVimeoSlide extends MetaSlide {

    public $identifier = "vimeo"; // should be lowercase, one word (use underscores if needed)
    public $name = "Vimeo"; // slide type title

    /**
     * Register slide type
     */
    public function __construct() {

        if ( is_admin() ) {
            add_filter( "media_upload_tabs", array( $this, 'custom_media_upload_tab_name' ), 999, 1 );
            add_action( "metaslider_save_{$this->identifier}_slide", array( $this, 'save_slide' ), 5, 3 );
            add_action( "media_upload_{$this->identifier}", array( $this, 'get_iframe' ) );
            add_action( "wp_ajax_create_{$this->identifier}_slide", array( $this, 'ajax_create_slide' ) );
            add_action( "metaslider_register_admin_styles", array( $this, 'register_admin_styles' ), 10, 1 );
        }

        add_filter( "metaslider_get_{$this->identifier}_slide", array( $this, 'get_slide' ), 10, 2 );

    }

    /**
     * Custom CSS Styling for vimeo slides
     */
    public function register_admin_styles() {

        wp_enqueue_style( "metasliderpro-{$this->identifier}-style", plugins_url( 'assets/style.css' , __FILE__ ), false, METASLIDERPRO_VERSION );
    
    }

    /**
     * Extract the slide setings
     */
    public function set_slide( $id ) {

        parent::set_slide( $id );
        $this->slide_settings = get_post_meta( $id, 'ml-slider_settings', true );

    }

    /**
     * Add extra tabs to the default wordpress Media Manager iframe
     *
     * @var array existing media manager tabs
     */
    public function custom_media_upload_tab_name( $tabs ) {

        // restrict our tab changes to the meta slider plugin page
        if ( ( isset( $_GET['page'] ) && $_GET['page'] == 'metaslider' ) ||
            ( isset( $_GET['tab'] ) && in_array( $_GET['tab'], array( $this->identifier ) ) ) ) {

            $newtabs = array(
                $this->identifier => __( $this->name, 'metasliderpro' )
            );

            return array_merge( $tabs, $newtabs );
        }

        return $tabs;

    }

    /**
     * Create a new slide and echo the admin HTML
     */
    public function ajax_create_slide() {

        $slider_id = intval( $_POST['slider_id'] );
        $fields['menu_order'] = 9999;
        $fields['video_id'] = $_POST['video_id'];
        $this->create_slide( $slider_id, $fields );
        echo $this->get_admin_slide();
        die(); // this is required to return a proper result

    }

    /**
     * Media Manager tab
     */
    public function vimeo_tab() {

        return $this->get_iframe();

    }

    /**
     * Get the thumbnail URL from the vimeo API
     */
    private function get_thumb_url( $id ) {

        $thumb = new WP_Http();
        $thumb = $thumb->request( "http://vimeo.com/api/v2/video/$id.php" );

        if ( !is_wp_error( $thumb ) && isset( $thumb['body'] ) ) {
            $body = unserialize( $thumb['body'] );

            if ( isset( $body[0]['thumbnail_medium'] ) ) {
                return $body[0]['thumbnail_medium'];
            }
        }

        return false;

    }

    /**
     * Create a new vimeo slide
     */
    public function create_slide( $slider_id, $fields ) {

        $this->set_slider( $slider_id );

        $postinfo = array(
            'post_title'=> "Meta Slider - Vimeo - {$fields['video_id']}",
            'post_mime_type' => 'image/jpeg',
            'post_status' => 'inherit',
            'post_content' => '',
            'guid' => "http://www.vimeo.com/{$fields['video_id']}",
            'menu_order' => $fields['menu_order'],
            'post_name' => $fields['video_id']
        );

        $thumb_url = $this->get_thumb_url( $fields['video_id'] );
        $vimeo_thumb = false;

        if ( $thumb_url ) {
            $vimeo_thumb = new WP_Http();
            $vimeo_thumb = $vimeo_thumb->request( $thumb_url );
        }

        if ( !$vimeo_thumb || is_wp_error( $vimeo_thumb ) || $vimeo_thumb['response']['code'] != 200 ) {
            $slide_id = wp_insert_attachment( $postinfo );
        } else {
            $attachment = wp_upload_bits( "vimeo_{$fields['video_id']}.jpg", null, $vimeo_thumb['body'] );
            $filename = $attachment['file'];
            $slide_id = wp_insert_attachment( $postinfo, $filename );
            $attach_data = wp_generate_attachment_metadata( $slide_id, $filename );
            wp_update_attachment_metadata( $slide_id, $attach_data );
        }

        // store the type as a meta field against the attachment
        $this->add_or_update_or_delete_meta( $slide_id, 'type', $this->identifier );
        $this->set_slide( $slide_id );
        $this->tag_slide_to_slider();

        return $slide_id;

    }

    /**
     * Admin slide html
     *
     * @return string html
     */
    protected function get_admin_slide() {

        $thumb = "";

        // only show a thumbnail if we managed to download one when the slide
        // was created
        $file_path = get_attached_file( $this->slide->ID );
        if ( strlen( $file_path ) ) {
            $thumb = $this->get_thumb();
        }

        $url = $this->slide->guid;
        sscanf( parse_url( $url, PHP_URL_PATH ), '/%d', $video_id );

        $badge_checked = isset( $this->slide_settings['badge'] ) && $this->slide_settings['badge'] == 'on' ? 'checked=checked' : '';
        $byline_checked = isset( $this->slide_settings['byline'] ) && $this->slide_settings['byline'] == 'on' ? 'checked=checked' : '';
        $portrait_checked = isset( $this->slide_settings['portrait'] ) && $this->slide_settings['portrait'] == 'on' ? 'checked=checked' : '';
        $title_checked = isset( $this->slide_settings['title'] ) && $this->slide_settings['title'] == 'on' ? 'checked=checked' : '';
        $autoPlay_checked = isset( $this->slide_settings['autoPlay'] ) && $this->slide_settings['autoPlay'] == 'on' ? 'checked=checked' : '';

        $row  = "<tr class='slide {$this->identifier} flex responsive'>";
        $row .= "    <td class='col-1'>";
        $row .= "        <div class='thumb' style='background-image: url({$thumb})'>";
        $row .= "            <a class='delete-slide confirm' href='?page=metaslider&id={$this->slider->ID}&deleteSlide={$this->slide->ID}'>x</a>";
        $row .= "            <span class='slide-details'>" . __( "Vimeo", 'metasliderpro' ) . "</span>";
        $row .= "            <span class='vimeo'></span>";
        $row .= "        </div>";
        $row .= "    </td>";
        $row .= "    <td class='col-2'>";
        $row .= "        <ul class='tabs'>";
        $row .= "            <li class='selected' rel='tab-1'>" . __( 'General', 'metasliderpro' ) . "</li>";
        $row .= "        </ul>";
        $row .= "        <div class='tabs-content'>";
        $row .= "            <div class='tab tab-1'>";
        $row .= "                <ul>";
        $row .= "                    <li><label><input type='checkbox' name='attachment[{$this->slide->ID}][settings][title]' {$title_checked}/>" . __( 'Show the title on the video', 'metasliderpro' ) ."</label></li>";
        $row .= "                    <li><label><input type='checkbox' name='attachment[{$this->slide->ID}][settings][badge]' {$badge_checked}/>" . __( 'Show the user badge on the video', 'metasliderpro' ) ."</label></li>";
        $row .= "                    <li><label><input type='checkbox' name='attachment[{$this->slide->ID}][settings][byline]' {$byline_checked}/>" . __( 'Show the user byline on the video', 'metasliderpro' ) ."</label></li>";
        $row .= "                    <li><label><input type='checkbox' name='attachment[{$this->slide->ID}][settings][portrait]' {$portrait_checked}/>" . __( 'Show the user portrait on the video', 'metasliderpro' ) ."</label></li>";
        $row .= "                    <li><label><input type='checkbox' name='attachment[{$this->slide->ID}][settings][autoPlay]' {$autoPlay_checked}/>" . __( 'Auto play', 'metasliderpro' ) ."</label></li>";
        $row .= "                </ul>";
        $row .= "            </div>";
        $row .= "        </div>";
        $row .= "        <input type='hidden' name='attachment[{$this->slide->ID}][type]' value='vimeo' />";
        $row .= "        <input type='hidden' class='menu_order' name='attachment[{$this->slide->ID}][menu_order]' value='{$this->slide->menu_order}' />";
        $row .= "    </td>";
        $row .= "</tr>";

        return $row;

    }

    /**
     * Public slide html
     *
     * @return string html
     */
    protected function get_public_slide() {

        add_filter( 'metaslider_flex_slider_parameters', array( $this, 'get_flex_slider_parameters' ), 10, 2 );
        add_filter( 'metaslider_flex_slider_javascript', array( $this, 'get_flex_vimeo_javascript' ), 10, 2 );

        add_filter( 'metaslider_responsive_slider_parameters', array( $this, 'get_responsive_slider_parameters' ), 10, 2 );
        add_filter( 'metaslider_responsive_slider_javascript', array( $this, 'get_responsive_vimeo_javascript' ), 10, 2 );

        wp_enqueue_script( 'metasliderpro-vimeo-api', plugins_url( 'assets/froogaloop/jQuery.froogaloop.min.js', __FILE__ ), array( 'jquery' ) );

        $settings = get_post_meta( $this->slider->ID, 'ml-slider_settings', true );
        $url = $this->slide->guid;
        sscanf( parse_url( $url, PHP_URL_PATH ), '/%d', $video_id ); // get the video ID
        $ratio = $this->settings['height'] / $this->settings['width'] * 100;

        if ( $this->settings['type'] == 'responsive' ) {
            return $this->get_responsive_slides_markup( $video_id, $settings, $ratio );
        }

        if ( $this->settings['type'] == 'flex' ) {
            return $this->get_flex_slider_markup( $video_id, $settings, $ratio );
        }

    }

    /**
     * Build the Vimeo iFrame URL based on the slide settings
     */
    public function get_vimeo_iframe_url( $video_id ) {

        $url = "//player.vimeo.com/video/{$video_id}?api=1&player_id=vimeo_{$this->slide->ID}";

        foreach ( array( 'badge', 'byline', 'title', 'portrait' ) as $param ) {
            $url .= '&' . $param . "=";

            if ( isset( $this->slide_settings[$param] ) && $this->slide_settings[$param] == 'on' ) {
                $url .= '1';
            } else {
                $url .= '0';
            }
        }

        $url = apply_filters( 'metaslider_vimeo_params', $url, $this->slider->ID, $this->slide->ID );

        return $url;

    }

    /**
     * Return the slide HTML
     */
    public function get_responsive_slides_markup( $video_id, $settings, $ratio ) {

        $url = $this->get_vimeo_iframe_url( $video_id );
        $autoPlay = isset( $this->slide_settings['autoPlay'] ) && $this->slide_settings['autoPlay'] == 'on' ? '1' : '0';

        $html  = "<div style='position: relative; padding-bottom: {$ratio}%; height: 0;'>";
        $html .= "<iframe class='vimeo' data-autoPlay='{$autoPlay}' id='vimeo_{$this->slide->ID}' width='{$settings['width']}' height='{$settings['height']}' src='{$url}' frameborder='0'></iframe>";
        $html .= "</div>";

        return $html;

    }

    /**
     * Return the slide HTML
     */
    public function get_flex_slider_markup( $video_id, $settings, $ratio ) {

        $url = $this->get_vimeo_iframe_url( $video_id );
        $autoPlay = isset( $this->slide_settings['autoPlay'] ) && $this->slide_settings['autoPlay'] == 'on' ? '1' : '0';

        $html  = "<div style='position: relative; padding-bottom: {$ratio}%; height: 0;'>";
        $html .= "<iframe class='vimeo' data-autoPlay='{$autoPlay}' id='vimeo_{$this->slide->ID}' width='{$settings['width']}' height='{$settings['height']}' src='{$url}' frameborder='0'></iframe>";
        $html .= "</div>";

        if ( version_compare( METASLIDER_VERSION, 2.3, '>=' ) ) {
            // store the slide details
            $slide = array(
                'id' => $this->slide->ID,
                'data-thumb' => ''
            );

            $slide = apply_filters( 'metaslider_vimeo_slide_attributes', $slide, $this->slider->ID, $this->settings );

            $thumb = strlen( $slide['data-thumb'] ) > 0 ? " data-thumb='" . $slide['data-thumb']  . "'" : '';

            $html = "<li class='slider-{$this->slider->ID} slide-{$this->slide->ID} ms-vimeo' style='display: none; float: left;'{$thumb}>" . $html . "</li>";
        }

        return $html;

    }

    /**
     * JavaScript to handle video interaction
     */
    public function get_flex_vimeo_javascript( $javascript, $slider_id ) {

        $html = "jQuery('#metaslider_{$slider_id} iframe.vimeo').each(function(i) {";
        $html .= "\n                if (is_first_slide(jQuery(this)) && is_autoplay(jQuery(this))) {";
        $html .= "\n                    jQuery('#metaslider_{$slider_id}').flexslider('pause');";
        $html .= "\n                }";
        $html .= "\n                var player = document.getElementById(jQuery(this).attr('id'));";
        $html .= "\n                Froogaloop(player).addEvent('ready', ready);";
        $html .= "\n            });";

        $html .= "\n            function is_autoplay(el) {";
        $html .= "\n                return el.attr('data-autoPlay') == 1;";
        $html .= "\n            }";

        $html .= "\n            function is_first_slide(el) {";
        $html .= "\n                return el.closest('li').index() == 0;";
        $html .= "\n            }";

        $html .= "\n            function addEvent(element, eventName, callback) {";
        $html .= "\n                if (element.addEventListener) {";
        $html .= "\n                    element.addEventListener(eventName, callback, false)";
        $html .= "\n                } else {";
        $html .= "\n                    element.attachEvent(eventName, callback, false);";
        $html .= "\n                }";
        $html .= "\n            }";

        $html .= "\n            function ready(player_id) {";
        $html .= "\n                var froogaloop = Froogaloop(player_id);";
        if ( $this->settings['autoPlay'] == 'true' ) {
            $html .= "\n                froogaloop.addEvent('pause', function(data) {"; // also fires on finish
            $html .= "\n                   jQuery('#metaslider_{$slider_id}').flexslider('play');";
            $html .= "\n                });";
        }
        $html .= "\n                froogaloop.addEvent('play', function(data) {";
        $html .= "\n                    jQuery('#metaslider_{$slider_id}').flexslider('stop');";
        $html .= "\n                    jQuery('#metaslider_{$slider_id} .flex-active-slide iframe.vimeo').attr('data-autoPlay', 0);";
        $html .= "\n                });";
        $html .= "\n                if (is_first_slide(jQuery('#' + player_id)) && is_autoplay(jQuery('#' + player_id))) {";
        $html .= "\n                    froogaloop.api('play');";
        $html .= "\n                }";
        $html .= "\n            }";


        // we don't want this filter hanging around if there's more than one slideshow on the page
        remove_filter( 'metaslider_flex_slider_javascript', array( $this, 'get_flex_vimeo_javascript' ) );

        return $javascript . $html;

    }

    /**
     * Modify the flex slider parameters when a vimeo slide has been added
     */
    public function get_flex_slider_parameters( $options, $slider_id ) {

        $options['useCSS'] = 'false';

        $options["before"][] = "    jQuery('#metaslider_{$slider_id} iframe.vimeo').each(function(index) {
                         Froogaloop(this).api('pause');
                    });";

        $options["after"][]  = "    jQuery('#metaslider_{$slider_id} .flex-active-slide iframe.vimeo[data-autoPlay=1]').each(function(index) {
                        Froogaloop(this).api('play');
                    });";

        // we don't want this filter hanging around if there's more than one slideshow on the page
        remove_filter( 'metaslider_flex_slider_parameters', array( $this, 'get_flex_slider_parameters' ) );

        return $options;

    }


    /**
     * JavaScript to handle video interaction
     */
    public function get_responsive_vimeo_javascript( $javascript, $slider_id ) {

        $html  = "\n            function addEvent(element, eventName, callback) {";
        $html .= "\n                if (element.addEventListener) {";
        $html .= "\n                    element.addEventListener(eventName, callback, false)";
        $html .= "\n                } else {";
        $html .= "\n                    element.attachEvent(eventName, callback, false);";
        $html .= "\n                }";
        $html .= "\n            }";
        $html .= "\n            function ready(player_id) {";
        $html .= "\n                var froogaloop = Froogaloop(player_id);";
        $html .= "\n                froogaloop.addEvent('play', function(data) {";
        $html .= "\n                    jQuery('#metaslider_{$slider_id} .rslides1_on iframe.vimeo').attr('data-autoPlay', 0);";
        $html .= "\n                });";
        $html .= "\n                jQuery('#metaslider_{$slider_id} .rslides1_on iframe.vimeo[data-autoPlay=1]').each(function(index) {";
        $html .= "\n                    froogaloop.api('play');";
        $html .= "\n                });";
        $html .= "\n            }";
        $html .= "\n            jQuery('#metaslider_{$slider_id} iframe.vimeo').each(function() {";
        $html .= "\n                var vimeo = jQuery(this);";
        $html .= "\n                var video_id = vimeo.attr('id');";
        $html .= "\n                var player = document.getElementById(video_id);";
        $html .= "\n                Froogaloop(player).addEvent('ready', ready);";
        $html .= "\n            });";


        // we don't want this filter hanging around if there's more than one slideshow on the page
        remove_filter( 'metaslider_responsive_slider_javascript', array( $this, 'get_responsive_vimeo_javascript' ) );

        return $javascript . $html;

    }

    /**
     * Modify the reponsive slider parameters when a vimeo slide has been added
     */
    public function get_responsive_slider_parameters( $options, $slider_id ) {

        // disable hoverpause - there is a bug with flex slider that means it
        // resumes the slideshow even when it has just been told to pause
        if ( isset( $options["pause"] ) ) {
            unset( $options["pause"] );
        }

        $options["auto"] = "false";
        $options["before"][] = "    jQuery('#metaslider_{$slider_id} iframe.vimeo').each(function(index) {
                        Froogaloop(this).api('pause');
                    });";
        $options["after"][]  = "    jQuery('#metaslider_{$slider_id} .rslides1_on iframe.vimeo[data-autoPlay=1]').each(function(index) {
                        Froogaloop(this).api('play');
                    });";
        // we don't want this filter hanging around if there's more than one slideshow on the page
        remove_filter( 'metaslider_responsive_slider_parameters', array( $this, 'get_responsive_slider_parameters' ) );

        return $options;

    }

    /**
     * Return wp_iframe
     */
    public function get_iframe() {

        return wp_iframe( array( $this, 'iframe' ) );

    }

    /**
     * Media Manager iframe HTML
     */
    public function iframe() {

        wp_enqueue_style( 'media-views' );
        wp_enqueue_style( "metasliderpro-{$this->identifier}-styles", plugins_url( 'assets/style.css' , __FILE__ ) );
        wp_enqueue_script( "metasliderpro-{$this->identifier}-script", plugins_url( 'assets/script.js' , __FILE__ ), array( 'jquery' ) );
        wp_localize_script( "metasliderpro-{$this->identifier}-script", 'metaslider_custom_slide_type', array(
                'identifier' => $this->identifier,
                'name' => $this->name
            ) );

        echo "<div class='metaslider'>
                <div class='vimeo'>
                    <div class='media-embed'>
                        <label class='embed-url'>
                            <input type='text' placeholder='http://vimeo.com/36820781' class='vimeo_url'>
                            <span class='spinner'></span>
                        </label>
                        <div class='embed-link-settings'></div>
                    </div>
                </div>
            </div>
            <div class='media-frame-toolbar'>
                <div class='media-toolbar'>
                    <div class='media-toolbar-primary'>
                        <a href='#' class='button media-button button-primary button-large' disabled='disabled'>" . __("Add to slider", "metasliderpro") . "</a>
                    </div>
                </div>
            </div>";

    }

    /**
     * Save
     */
    protected function save( $fields ) {

        wp_update_post( array(
                'ID' => $this->slide->ID,
                'menu_order' => $fields['menu_order']
            ) );

        $this->add_or_update_or_delete_meta( $this->slide->ID, 'settings', $fields['settings'] );
        
    }
}
?>
