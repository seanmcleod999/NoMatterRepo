<?php get_header(); ?>

<div id="outerContainer"> 
    <div id="container">

        <div id="containerPadding" class="clearfix">

            <div id="content"> 

                <div id="contentPage" class="NonHomePageContent TopDrippingPaint">

                    <h1>KID FONQUE LIVE</h1>

                    <div>         
                        <?php echo do_shortcode('[metaslider id=199]') ?>
                    </div>

                    <?php
                        $today = date('Ymd');
                        $args = array(
                            'post_type' => 'gigs',
                            'posts_per_page' => '20',
                            'orderby' => 'meta_value',
                            'meta_key' => 'gig_date',
                            'order' => 'DESC'
                        );

                        $events_query = new WP_Query($args); ?>

			        <?php while ( $events_query->have_posts() ) : $events_query->the_post(); ?>
				        <div id="post-<?php the_ID(); ?>" <?php post_class(); ?>>

                            <h3><span class="gigdate"><?php echo mysql2date('l, F j, Y', get_post_meta($post->ID, 'gig_date', $single = true)); ?></span></h3>


                            <div id="ArchiveGigDetails">
                        
                                

                                <ul id="sm-event-cal-list-archive"> 

                                    <li>

                                        <a href="<?php echo get_permalink($post->ID); ?>">

                                            <div id="sm-event-cal-list-gigdetails">

                                                <span><?php the_title() ?> </span>

                                                <span> @ <?php echo get_post_meta($post->ID, 'gig_venue', $single = true);?>,&nbsp;</span>

                                                <span><?php echo get_post_meta($post->ID, 'gig_city', $single = true); ?></span>

                                            </div>
                                           
                                            <div class=\"clear\"><!-- --></div>
                                        </a>

                                    </li>

                                </ul>
                        
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
