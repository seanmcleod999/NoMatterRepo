<?php get_header(); ?>

<div id="outerContainer"> 
    <div id="container">
        <div id="containerPadding" class="clearfix">
            
            <div id="content" class="">

                 <div id="contentPage" class="NonHomePageContent TopDrippingPaint2">
               
                    <h1>DISCOGRAPHY</h1>

                    <?php
                        $args = array(
                            'post_type' => 'album',
                            'orderby' => 'meta_value',
                            'meta_key' => 'date_released',
                            'order' => 'DESC'
                        );

                        $albums_query = new WP_Query($args); ?>
 
				    <?php while ( $albums_query->have_posts() ) : $albums_query->the_post(); ?>

				        <div id="post-<?php the_ID(); ?>" <?php post_class(); ?>>

                            <div id="ArchiveAlbum" class="clearfix">

                                <div id="ArchiveAlbumCoverPicture">
                        
                                        <?php 
                                        $imageid = get_post_meta($post->ID, 'album_cover', $single = true);                                          
                                        ?>

                                        <a href="<?php echo get_permalink($post->ID); ?>"><?php echo wp_get_attachment_image($imageid, 'medium') ?></a> 
                        
                                </div>

                                <div id="ArchiveAlbumInformation">
                        
                                    <h2 class="entry-title"><?php the_title(); ?></h2>
                        
                                    <span class="AlbumArtist"><?php echo get_post_meta($post->ID, 'artist', $single = true); ?></span>
                                    <span class="AlbumRecordLabele"><?php echo get_post_meta($post->ID, 'record_label', $single = true); ?></span>
                                    <span class="AlbumYearReleased"><?php echo mysql2date('Y', get_post_meta($post->ID, 'date_released', $single = true)); ?></span>

                                    <a href="<?php echo get_permalink($post->ID); ?>" class="button red">more details</a> 

                                </div>

                            </div>
			                  				                 
				            </div><!-- #post-<?php the_ID(); ?> -->
                                                              
				    <?php endwhile; ?>            
			      </div><!-- #contentPage -->           
            </div><!-- #content -->
            <div class="clear"><!-- --></div>
            <?php get_sidebar(); ?>
        </div><!-- #containerPadding -->
    </div><!-- #container -->

</div><!-- #outerContainer -->
     
<?php get_footer(); ?>