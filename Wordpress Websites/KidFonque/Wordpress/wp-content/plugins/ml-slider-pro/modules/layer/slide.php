<?php

// disable direct access
if ( ! defined( 'ABSPATH' ) ) exit;

/**
 * HTML Overlay Slide - HTML placed over an image.
 *
 * Renamed on the public side to "Layer Slide"
 */
class MetaLayerSlide extends MetaSlide {

    public $identifier = "html_overlay"; // should be lowercase, one word (use underscores if needed)
    public $name = "Layer Slide"; // slide type title

    /**
     * Register slide type
     */
    public function __construct() {

        if ( is_admin() ) {
            add_filter( "metaslider_advanced_settings", array( $this, 'add_downscale_only_setting' ), 10, 2 );
            add_action( "metaslider_save_{$this->identifier}_slide", array( $this, 'save_slide' ), 5, 3 );
            add_action( "wp_ajax_create_{$this->identifier}_slide", array( $this, 'ajax_create_slide' ) );
            add_action( "metaslider_register_admin_scripts", array( $this, 'register_admin_scripts' ), 10, 1 );
            add_action( 'metaslider_register_admin_styles', array( $this, 'register_admin_styles' ), 10, 1 );
            add_filter( 'media_view_strings', array( $this, 'custom_media_uploader_tabs' ), 10, 1 );
        }

        add_filter( "metaslider_get_{$this->identifier}_slide", array( $this, 'get_slide' ), 10, 2 );
    }

    /**
     *
     */
    public function add_downscale_only_setting( $aFields, $slider ) {
        $newFields = array(
            'layer_scaling' => array(
                'priority' => 240,
                'type' => 'select',
                'value' => $slider->get_setting( 'layer_scaling' ),
                'label' => __( "Layer Scaling", "metasliderpro" ),
                'class' => 'effect flex responsive',
                'helptext' => __( "Select responsiver layer scaling behaviour", "metasliderpro" ),
                'options' => array(
                    'up_and_down'             => array( 'class' => 'option flex responsive' , 'label' => __( "Scale Up & Down", "metaslider" ) ),
                    'down_only'              => array( 'class' => 'option flex responsive', 'label' => __( "Scale Down Only", "metaslider" ) )
                ),
            )
        );

        return array_merge( $aFields, $newFields );
    }
    /**
     * Creates a new media manager tab
     *
     * @param array   $strings registered media manager tabs
     *
     * @return array
     */
    public function custom_media_uploader_tabs( $strings ) {
        $strings['insertHtmlOverlay'] = __( 'Layer Slide', 'metasliderpro' );
        return $strings;
    }

    /**
     * Registers and enqueues admin CSS
     */
    public function register_admin_styles() {
        wp_enqueue_style( 'metasliderpro-codemirror-style', plugins_url( 'assets/codemirror/lib/codemirror.css' , __FILE__ ), false, METASLIDERPRO_VERSION );
        wp_enqueue_style( 'metasliderpro-codemirror-theme-style', plugins_url( 'assets/codemirror/theme/monokai.css' , __FILE__ ), false, METASLIDERPRO_VERSION );
        wp_enqueue_style( "metasliderpro-{$this->identifier}-style", plugins_url( 'assets/style.css' , __FILE__ ), false, METASLIDERPRO_VERSION );
        wp_enqueue_style( 'metasliderpro-spectrum-style', plugins_url( 'assets/spectrum/spectrum.css' , __FILE__ ), false, METASLIDERPRO_VERSION );
    }

    /**
     * Registers and enqueues admin JavaScript
     */
    public function register_admin_scripts() {
        wp_enqueue_style( 'metasliderpro-spectrum-style', METASLIDERPRO_ASSETS_URL . 'spectrum/spectrum.css', false, METASLIDERPRO_VERSION );

        wp_enqueue_script( 'metasliderpro-html-overlay-script', plugins_url( 'assets/html_overlay.js' , __FILE__ ), array( 'jquery', 'media-views', 'metaslider-admin-script' ), METASLIDERPRO_VERSION );
        wp_enqueue_script( 'metasliderpro-layer-editor-script', plugins_url( 'assets/layer_editor.js' , __FILE__ ), array( 'jquery', 'media-views', 'metaslider-admin-script' ), METASLIDERPRO_VERSION );
        wp_enqueue_script( 'metasliderpro-codemirror-lib', plugins_url( 'assets/codemirror/lib/codemirror.js' , __FILE__ ), array(), METASLIDERPRO_VERSION );
        wp_enqueue_script( 'metasliderpro-codemirror-xml', plugins_url( 'assets/codemirror/mode/xml/xml.js' , __FILE__ ), array(), METASLIDERPRO_VERSION );
        wp_enqueue_script( 'jquery-ui-resizable' );
        wp_enqueue_script( 'jquery-ui-draggable' );
        wp_enqueue_script( 'ckeditor', plugins_url( 'assets/ckeditor/ckeditor.js' , __FILE__ ), array( 'jquery' ), METASLIDERPRO_VERSION );
        wp_enqueue_script( 'metasliderpro-spectrum', plugins_url( 'assets/spectrum/spectrum.js' , __FILE__ ), array(), METASLIDERPRO_VERSION );

        // localise the JS
        wp_localize_script( 'metasliderpro-layer-editor-script', 'metasliderpro', array(
                'newLayer' => __( "New Layer", 'metasliderpro' ),
                'addLayer' => __( "Add Layer", 'metasliderpro' ),
                'duplicateLayer' => __( "Duplicate Layer", 'metasliderpro' ),
                'save' => __( "Save", 'metasliderpro' ),
                'saveChanges' => __( "Save changes?", 'metasliderpro' ),
                'animation' => __( "Animation", 'metasliderpro' ),
                'styling' => __( "Styling", 'metasliderpro' ),
                'px' => __( "px", 'metasliderpro' ),
                'animation' => __( "Animation", 'metasliderpro' ),
                'wait' => __( "Wait", 'metasliderpro' ),
                'thenWait' => __( "then wait", 'metasliderpro' ),
                'secondsAnd' => __( "seconds and", 'metasliderpro' ),
                'padding' => __( "Padding", 'metasliderpro' ),
                'background' => __( "Background", 'metasliderpro' ),
                'areYouSure' => __( "Are you sure?", 'metasliderpro' ),
                'snapToGrid' => __( "Snap to grid", 'metasliderpro' ),
                'layerLink' => __( "Link to", 'metasliderpro' ),
                'ck_editor_font_list' => apply_filters( "metaslider_layer_editor_fonts", "" ),
                'setWidth' => __( "Please set a width in the slideshow settings", 'metasliderpro' ),
                'setHeight' => __( "Please set a height in the slideshow settings", 'metasliderpro' ),
                'addToSlider' => __( "Add to slider", 'metasliderpro' ),
                'noLayerSelected' => __( "Warning: No layer selected. Please click on a layer to select it before applying changes.", 'metasliderpro')
            ) );
    }

    /**
     * Create a new layer slide.
     *
     * @return string - HTML for the created slide
     */
    public function ajax_create_slide() {
        $slide_id = intval( $_POST['slide_id'] );
        $slider_id = intval( $_POST['slider_id'] );

        $this->set_slider( $slider_id );

        // duplicate the attachment - get the source slide
        $attachment = get_post( $slide_id, ARRAY_A );
        unset( $attachment['ID'] );
        unset( $attachment['post_parent'] );
        unset( $attachment['post_date'] );
        unset( $attachment['post_date_gmt'] );
        unset( $attachment['post_modified'] );
        unset( $attachment['post_modified_gmt'] );

        $attachment['post_title'] = 'Meta Slider - HTML Overlay - ' . $attachment['post_title'];

        // insert a new attachment
        $new_slide_id = wp_insert_post( $attachment );

        // copy over the custom fields
        $custom_fields = get_post_custom( $slide_id );

        foreach ( $custom_fields as $key => $value ) {
            if ( $key != '_wp_attachment_metadata' ) {
                update_post_meta( $new_slide_id, $key, $value[0] );
            }
        }

        // update metadata (regen thumbs also)
        $data = wp_get_attachment_metadata( $slide_id );

        wp_update_attachment_metadata( $new_slide_id, $data );

        // store the file type
        $this->add_or_update_or_delete_meta( $new_slide_id, 'type', 'html_overlay' );

        // set current slide to our newly duplicated slide
        $this->set_slide( $new_slide_id );

        // tag the new slide to the slider
        $this->tag_slide_to_slider();

        // finally, return the admin table row HTML
        echo $this->get_admin_slide();
        die();
    }

    /**
     * Return the admin slide HTML
     *
     * @return string
     */
    protected function get_admin_slide() {
        $thumb       = $this->get_thumb();
        $html        = get_post_meta( $this->slide->ID, 'ml-slider_html', true );
        $url         = get_post_meta( $this->slide->ID, 'ml-slider_url', true );
        $title       = get_post_meta( $this->slide->ID, 'ml-slider_title', true );
        $alt         = get_post_meta( $this->slide->ID, '_wp_attachment_image_alt', true );
        $target      = get_post_meta( $this->slide->ID, 'ml-slider_new_window', true ) == 'true' ? 'checked=checked' : '';

        $imageHelper = new MetaSliderImageHelper(
            $this->slide->ID,
            $this->settings['width'],
            $this->settings['height'],
            isset( $this->settings['smartCrop'] ) ? $this->settings['smartCrop'] : 'false'
        );

        $background_url = $imageHelper->get_image_url();

        // localisation
        $str_new_window = __( "New Window", 'metasliderpro' );
        $str_url        = __( "URL", 'metasliderpro' );

        $row  = "<tr class='slide layer_slide flex responsive'>";
        $row .= "    <td class='col-1'>";
        $row .= "        <div class='thumb' style='background-image: url({$thumb})'>";
        $row .= "            <a class='delete-slide confirm' href='?page=metaslider&id={$this->slider->ID}&deleteSlide={$this->slide->ID}'>x</a>";
        $row .= "            <span class='slide-details'>" . __( "Layer Slide", 'metasliderpro' ) . "</span>";
        $row .= "        </div>";
        $row .= "    </td>";
        $row .= "    <td class='col-2'>";
        $row .= "        <ul class='tabs'>";
        $row .= "            <li class='selected' rel='tab-1'>" . __( "General", 'metasliderpro' ) . "</li>";
        $row .= "            <li rel='tab-2'>" . __( "SEO", 'metasliderpro' ) . "</li>";
        $row .= "            <li rel='tab-3'>" . __( "Extra", 'metasliderpro' ) . "</li>";
        $row .= "            <li rel='tab-4' class='codemirror' data-editor='editor{$this->slide->ID}'>" . __( "Edit Source", 'metasliderpro' ) . "</li>";
        $row .= "        </ul>";
        $row .= "        <div class='tabs-content'>";
        $row .= "            <div class='tab tab-1'>";
        $row .= "                <button class='openLayerEditor button button-primary' data-thumb='{$background_url}' data-width='{$this->settings['width']}' data-height='{$this->settings['height']}' data-editor_id='editor{$this->slide->ID}'>Launch Layer Editor</button>";
        $row .= "                <div class='rawEdit'></div>"; // vantage backwards compatibility
        $row .= "            </div>";
        $row .= "            <div class='tab tab-2' style='display: none;'>";
        $row .= "                <div class='row'><label>" . __( "Background Image Title Text", "metasliderpro" ) . "</label></div>";
        $row .= "                <div class='row'><input type='text' size='50' name='attachment[{$this->slide->ID}][title]' value='{$title}' /></div>";
        $row .= "                <div class='row'><label>" . __( "Background Image Alt Text", "metasliderpro" ) . "</label></div>";
        $row .= "                <div class='row'><input type='text' size='50' name='attachment[{$this->slide->ID}][alt]' value='{$alt}' /></div>";
        $row .= "            </div>";
        $row .= "            <div class='tab tab-3' style='display: none;'>";
        $row .= "                <div class='row'><label>" . __( "Background Image Link", "metasliderpro" ) . "</label></div>";
        $row .= "                <input class='url' type='text' name='attachment[{$this->slide->ID}][url]' placeholder='{$str_url}' value='{$url}' />";
        $row .= "                <div class='new_window'>";
        $row .= "                    <label>{$str_new_window}<input type='checkbox' name='attachment[{$this->slide->ID}][new_window]' {$target} /></label>";
        $row .= "                </div>";
        $row .= "            </div>";
        $row .= "            <div class='tab tab-4' style='display: none;'>";
        $row .= "                <textarea class='wysiwyg' id='editor{$this->slide->ID}' name='attachment[{$this->slide->ID}][html]'>{$html}</textarea>";
        $row .= "            </div>";
        $row .= "        </div>";
        $row .= "        <input type='hidden' name='attachment[{$this->slide->ID}][type]' value='html_overlay' />";
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
        add_filter( 'metaslider_responsive_slider_javascript', array( $this, 'get_responsive_javascript' ), 10, 2 );
        add_filter( 'metaslider_flex_slider_javascript', array( $this, 'get_responsive_javascript' ), 10, 2 );
        add_filter( 'metaslider_responsive_slider_parameters', array( $this, 'get_responsive_slider_parameters' ), 10, 2 );
        add_filter( 'metaslider_flex_slider_parameters', array( $this, 'get_flex_slider_parameters' ), 10, 2 );
        add_action( 'metaslider_register_public_styles', array( $this, 'enqueue_assets' ) );

        if ( $this->settings['type'] == 'responsive' ) {
            return $this->get_responsive_slides_markup();
        }

        if ( $this->settings['type'] == 'flex' ) {
            return $this->get_flex_slider_markup();
        }
    }

    /**
     * Return the HTML Overlay portion of the slide
     */
    private function html_overlay() {
        $layer_html = get_post_meta( $this->slide->ID, 'ml-slider_html', true );
        $target = get_post_meta( $this->slide->ID, 'ml-slider_new_window', true ) ? '_blank' : '_self';
        $url = get_post_meta( $this->slide->ID, 'ml-slider_url', true );
        $slide_link = strlen( $url ) ? " data-link='{$url}' data-target='{$target}'" : '';
        if (strlen($layer_html)) {
            return "<div class='msHtmlOverlay' style='position: absolute; top: 0; left: 0; width: 100%; height: 100%;' {$slide_link}>" . __( do_shortcode( $layer_html ) ) . "</div>";
        }

        return "";
    }

    /**
     * Return the background image markup for the slide
     */
    private function image_tag() {
        $imageHelper = new MetaSliderImageHelper(
            $this->slide->ID,
            $this->settings['width'],
            $this->settings['height'],
            isset( $this->settings['smartCrop'] ) ? $this->settings['smartCrop'] : 'false'
        );

        $slide = array(
            'src' => $imageHelper->get_image_url(),
            'alt' => get_post_meta( $this->slide->ID, '_wp_attachment_image_alt', true ),
            'title' => get_post_meta( $this->slide->ID, 'ml-slider_title', true ),
            'class' => 'msDefaultImage',
            'height' => $this->settings['height'],
            'width' => $this->settings['width']
        );

        return $this->build_image_tag( $slide );
    }

    /**
     * Return the slide HTML for responsive slides
     *
     * @return string
     */
    private function get_responsive_slides_markup() {
        $html = $this->html_overlay();
        $html .= $this->image_tag();

        return $html;
    }

    /**
     * Return the slide HTML for flex slider
     *
     * @return string
     */
    private function get_flex_slider_markup() {
        $html = $this->image_tag();
        $html .= $this->html_overlay();

        if ( version_compare( METASLIDER_VERSION, 2.3, '>=' ) ) {
            // store the slide details
            $slide = array(
                'id' => $this->slide->ID,
                'data-thumb' => ''
            );

            $slide = apply_filters( 'metaslider_layer_slide_attributes', $slide, $this->slider->ID, $this->settings );

            $thumb = strlen( $slide['data-thumb'] ) > 0 ? " data-thumb='" . $slide['data-thumb']  . "'" : '';

            $html = "<li class='slider-{$this->slider->ID} slide-{$this->slide->ID} ms-layer' style='display: none; float: left;'{$thumb}>" . $html . "</li>";
        }

        return $html;
    }

    /**
     * Enqueue required public assets
     */
    public function enqueue_assets() {
        wp_enqueue_style( 'metaslider-pro-animate', plugins_url( 'assets/animate/animate.css' , __FILE__ ), false, METASLIDERPRO_VERSION );
        wp_enqueue_script( 'metaslider-pro-scale-layers', METASLIDERPRO_ASSETS_URL . 'public.js', false, METASLIDERPRO_VERSION );
    }

    /**
     * Reset CSS3 Animations when navigating between slides.
     *
     * @param array   $options   The JavaScript options for the slideshow
     * @param int     $slider_id Slideshow ID
     *
     * @return array $options Modified JavaScript options
     */
    public function get_responsive_slider_parameters( $options, $slider_id ) {
        $options["before"][] = "    $('#metaslider_{$slider_id} .animated').each(function(index) {
                         var el = $(this);
                         var cloned = el.clone();
                         el.before(cloned);
                         $(this).remove();
                    });";

        return $options;
    }

    /**
     * Reset CSS3 Animations when navigating between slides.
     *
     * @param array   $options
     * @param int     $slider_id
     *
     * @return array
     */
    public function get_flex_slider_parameters( $options, $slider_id ) {

        $options["before"][] = "    $('#metaslider_{$slider_id} li:not(\".flex-active-slide\") .animated').each(function(index) {
                        var el = $(this);
                        var cloned = el.clone();
                        el.before(cloned);
                        $(this).remove();
                    });";

        return $options;
    }

    /**
     * Return the javascript which creates the YouTube videos in the slideshow
     *
     * @param string  $javascript
     * @param int     $slider_id
     *
     * @return string
     */
    public function get_responsive_javascript( $javascript, $slider_id ) {


        $downscale_only = $this->settings['layer_scaling'] == 'down_only' ? 'true' : 'false';

        $html = "\n            jQuery(window).resize(function(){
               jQuery('#metaslider_{$slider_id}').metaslider_scale_layers({
                   downscale_only: {$downscale_only},
                   orig_width: {$this->settings['width']}
               });
            });
            jQuery('#metaslider_{$slider_id}').metaslider_scale_layers({
                downscale_only: {$downscale_only},
                orig_width: {$this->settings['width']}
            });";

        // we don't want this filter hanging around if there's more than one slideshow on the page
        remove_filter( 'metaslider_flex_slider_javascript', array( $this, 'get_responsive_javascript' ) );
        remove_filter( 'metaslider_responsive_slider_javascript', array( $this, 'get_responsive_javascript' ) );

        return $javascript . $html;
    }


    /**
     * Save
     *
     * @param array   $fields
     */
    protected function save( $fields ) {
        wp_update_post( array(
                'ID' => $this->slide->ID,
                'menu_order' => $fields['menu_order']
            ) );

        // store the URL as a meta field against the attachment
        $this->add_or_update_or_delete_meta( $this->slide->ID, 'url', $fields['url'] );

        $this->add_or_update_or_delete_meta( $this->slide->ID, 'title', $fields['title'] );

        if ( isset( $fields['alt'] ) ) {
            update_post_meta( $this->slide->ID, '_wp_attachment_image_alt', $fields['alt'] );
        }

        // store the 'new window' setting
        $new_window = isset( $fields['new_window'] ) && $fields['new_window'] == 'on' ? 'true' : 'false';

        $this->add_or_update_or_delete_meta( $this->slide->ID, 'new_window', $new_window );

        $this->add_or_update_or_delete_meta( $this->slide->ID, 'html', $fields['html'] );
    }
}
?>
