<?php get_header(); ?>
     <div id="outerContainer"> 

        <div id="container" class="clearfix">

            <div id="containerPadding" class="clearfix">

                <div id="content">

                    <div id="contentPage" class="NonHomePageContent">

				        <?php the_post(); ?>

                        <div id="SingleGigPost" >

                            <div id="post-<?php the_ID(); ?>" <?php post_class(); ?>>

                                <h2><span class="gigdate"><?php echo mysql2date('l, F j, Y', get_post_meta($post->ID, 'gig_date', $single = true)); ?></span></h2>

					            <h1 class="entry-title"><?php the_title(); ?></h1>

                                <h3>@ <?php echo get_post_meta($post->ID, 'gig_venue', $single = true);?>, <?php echo get_post_meta($post->ID, 'gig_city', $single = true); ?></h3>
                                <br />

                                 <?php 
                                $content = get_post_meta( $post->ID, 'gig_details', true );
                                echo wpautop( $content );
                                ?>

                                <div id="SingleGigFlyer">                        
                                        <?php 
                                        $imageid = get_post_meta($post->ID, 'gig_flyer', $single = true); 
                                        echo wp_get_attachment_image($imageid, 'large');  
                                        ?>                       
                                </div>
	

                            </div><!-- #post-<?php the_ID(); ?> -->  
                    
                        </div>           
            
 				        <?php comments_template('', true); ?>
                    </div><!-- #contentPage -->
                </div><!-- #content -->
                <?php get_sidebar(); ?>
            </div><!-- #containerPadding -->
        </div><!-- #container -->
   </div><!-- #outerContainer -->

<?php get_footer(); ?>
