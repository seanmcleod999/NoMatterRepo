<?php get_header(); ?>
    <div id="outerContainer"> 

        <div id="container">

            <div id="containerPadding" class="clearfix">

                <div id="content">

                    <div id="contentPage" class="NonHomePageContent">

				        <?php the_post(); ?>

                        <div id="post-<?php the_ID(); ?>" <?php post_class(); ?>>
					   
                            <div id="SingleAlbumTop" class="clearfix">
                       
                                <h2 class="entry-title"><?php the_title(); ?></h2>
                                <br/>

                                <div id="ArchiveAlbumCoverPicture">                        
                                        <?php 
                                        $imageid = get_post_meta($post->ID, 'album_cover', $single = true); 
                                        echo wp_get_attachment_image($imageid, 'large');  
                                        ?>                       
                                </div>

                                <div id="ArchiveAlbumInformation">                                                        
                        
                                    <span class="AlbumArtist"><?php echo get_post_meta($post->ID, 'artist', $single = true); ?></span>
                                    <span class="AlbumRecordLabel">Label: <?php echo get_post_meta($post->ID, 'record_label', $single = true); ?></span>
                                    <span class="AlbumYearReleased">Year: <?php echo mysql2date('Y', get_post_meta($post->ID, 'date_released', $single = true)); ?></span>
                               
                                </div>

                            </div> <!-- #SingleAlbumTop -->

                            <div id="SingleAlbumBottom">
                                <?php echo get_post_meta($post->ID, 'album_details', $single = true); ?>
                            </div>
										
                        </div><!-- #post-<?php the_ID(); ?> -->           
                
 				        <?php comments_template('', true); ?>

                    </div><!-- #contentPage -->

                </div><!-- #content -->
                <?php get_sidebar(); ?>
            </div><!-- #containerPadding -->
        </div><!-- #container -->
    </div><!-- #outerContainer -->


<?php get_footer(); ?>