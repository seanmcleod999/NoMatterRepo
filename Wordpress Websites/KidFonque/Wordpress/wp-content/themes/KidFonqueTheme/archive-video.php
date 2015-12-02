<?php get_header(); ?>
    <div id="outerContainer"> 
        <div id="container">
            <div id="containerPadding" class="clearfix">

                <div id="content"> 

                    <div id="contentPage" class="NonHomePageContent TopDrippingPaint">

                        <h1>YOUTUBE VIDEOS </h1>

			            <?php while ( have_posts() ) : the_post(); ?>
				            <div id="post-<?php the_ID(); ?>" <?php post_class(); ?>>

                                <div id="ArchiveVideoTitle">                           
                                    <h3><?php the_title() ?></h3>                           
                                </div>
                                <div id="ArchiveVideoWrapper">
                                    <?php echo do_shortcode('[fve]'. get_post_meta($post->ID, 'video_url', $single = true) .'[/fve]'); ?>
			                    </div>  
                                <div id="ArchiveVideoDetails">
                                    <?php echo get_post_meta($post->ID, 'details', $single = true); ?>
			                    </div>    				                 
				            </div><!-- #post-<?php the_ID(); ?> -->                                                    
			            <?php endwhile; ?> 
                
                    </div><!-- #contentPage -->              				             
                </div><!-- #content -->      
                <?php get_sidebar(); ?>
            </div><!-- #containerPadding -->
        </div><!-- #container -->
    </div><!-- #outerContainer -->
<?php get_footer(); ?>
