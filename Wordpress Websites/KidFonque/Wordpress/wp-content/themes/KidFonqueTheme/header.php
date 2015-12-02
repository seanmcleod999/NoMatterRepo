
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" <?php language_attributes(); ?>>
<head profile="http://gmpg.org/xfn/11">
    <meta name="viewport" content="width=device-width">
    <title><?php
        if ( is_single() ) { single_post_title(); }
        elseif ( is_home() || is_front_page() ) { bloginfo('name'); print ' | '; bloginfo('description'); get_page_number(); }
        elseif ( is_page() ) { single_post_title(''); }
        elseif ( is_search() ) { bloginfo('name'); print ' | Search results for ' . wp_specialchars($s); get_page_number(); }
        elseif ( is_404() ) { bloginfo('name'); print ' | Not Found'; }
        else { bloginfo('name'); wp_title('|'); get_page_number(); }
    ?></title>

 
    <link rel="SHORTCUT ICON" href="/wp-content/themes/KidFonqueTheme/images/favicon.ico" />

    <meta http-equiv="content-type" content="<?php bloginfo('html_type'); ?>; charset=<?php bloginfo('charset'); ?>" />
 
    <link rel="stylesheet" type="text/css" href="<?php bloginfo('stylesheet_url'); ?>" />
 
    <?php if ( is_singular() ) wp_enqueue_script( 'comment-reply' ); ?>

    <?php wp_head(); ?>
 
    <link rel="alternate" type="application/rss+xml" href="<?php bloginfo('rss2_url'); ?>" title="<?php printf( __( '%s latest posts', 'hbd-theme' ), wp_specialchars( get_bloginfo('name'), 1 ) ); ?>" />
    <link rel="alternate" type="application/rss+xml" href="<?php bloginfo('comments_rss2_url') ?>" title="<?php printf( __( '%s latest comments', 'hbd-theme' ), wp_specialchars( get_bloginfo('name'), 1 ) ); ?>" />
    <link rel="pingback" href="<?php bloginfo('pingback_url'); ?>" />
</head>


<body>
    <div id="fb-root"></div>
    <script>
        window.fbAsyncInit = function () {
            FB.init({
                appId: '580929885357452', // App ID
                channelUrl: '//www.kidfonque.co.za/channel.html', // Channel File
                status: true, // check login status
                cookie: true, // enable cookies to allow the server to access the session
                xfbml: true,  // parse XFBML
                oauth: true               
            });
        };

        // Load the SDK Asynchronously
        (function (d) {
            var js, id = 'facebook-jssdk', ref = d.getElementsByTagName('script')[0];
            if (d.getElementById(id)) { return; }
            js = d.createElement('script'); js.id = id; js.async = true;
            js.src = "//connect.facebook.net/en_US/all.js";
            ref.parentNode.insertBefore(js, ref);
        } (document));
</script>


    <script type="text/javascript">// <![CDATA[
    jQuery(document).ready(function($){

	    /* toggle nav */
	    $("#mobile-menu").on("click", function(){
		    $("#headerNav").slideToggle();
		    $(this).toggleClass("active");
	    });
    });
    // ]]></script>
    <div id="wrapper">
        <div id="header">

            <div id="mobile-menu"></div>

            <div id="headerNav">
			    <?php wp_nav_menu( array( 'sort_column' => 'menu_order', 'container_class' => 'menu-header' ) ); ?>
            </div><!-- #headerNav -->

           

            <div id="headerLogo">
                <img src="/wp-content/themes/KidFonqueTheme/images/KidFonque-logo-horizontal.png" alt="Kid Fonque"/>
            </div><!-- #headerLogo -->

            <?php
            $GLOBALS['HeaderImageNumber'] = rand(1,5);
            //$GLOBALS['HeaderImageNumber'] = 3;

            ?>

            <img src="/wp-content/themes/KidFonqueTheme/images/kidfonque-header-<?php echo $GLOBALS['HeaderImageNumber']?>.jpg" alt="Header Image" />

        </div><!-- #header -->

   
 


