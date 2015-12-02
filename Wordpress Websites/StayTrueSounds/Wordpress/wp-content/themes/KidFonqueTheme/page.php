<?php get_header(); ?>

<div id="outerContainer"> 
    <div id="container">
        <div id="containerPadding" class="clearfix">

            <?php $pageClass = "" ?>

            <?php if ( is_front_page() ) { ?>
            <div>         
                <?php echo do_shortcode('[metaslider id=96]')  ?>
            </div>
            <?php } ?>

            <?php if ( !is_front_page() ) { ?>
            <div>         
                <?php $pageClass = "NonHomePageContent" ?>
            </div>
            <?php } ?>

            <div id="content">

                <div id="contentPage" class="<?php echo $pageClass  ?>">
 
                    <?php the_post(); ?>
 
                    <div id="post-<?php the_ID(); ?>" <?php post_class(); ?>>
                    
                        <div class="entry-content">
                            <?php the_content(); ?>
                            <?php wp_link_pages('before=<div class="page-link">' . __( 'Pages:', 'your-theme' ) . '&after=</div>') ?>
                      
                        </div><!-- .entry-content -->

                    </div><!-- #post-<?php the_ID(); ?> -->           
 
                    <?php if ( get_post_custom_values('comments') ) comments_template() // Add a custom field with Name and Value of "comments" to enable comments on this page ?>            
 
                </div><!-- #contentPage -->

            </div><!-- #content -->
            
	        <?php get_sidebar(); ?>

        </div><!-- #containerPadding -->
    </div><!-- #container -->
</div><!-- #outerContainer -->
 
<?php get_footer(); ?>