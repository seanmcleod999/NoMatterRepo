<?php get_header(); ?>
 
        <div id="container">
            <div id="content">

				<?php the_post(); ?>

                <div id="post-<?php the_ID(); ?>" <?php post_class(); ?>>

					<h1 class="entry-title"><?php the_title(); ?></h1>
                    <h2 class="entry-title"><?php echo get_post_meta($post->ID, 'Artist', $single = true); ?></h2>
                    <h2 class="entry-title"><?php echo get_post_meta($post->ID, 'Record Label', $single = true); ?></h2>
                    <h2 class="entry-title"><?php echo get_post_meta($post->ID, 'Year', $single = true); ?></h2>

                     <?php the_post_thumbnail(); ?>
					
					<div class="entry-content">
						<?php the_content(); ?>
						<?php wp_link_pages('before=<div class="page-link">' . __( 'Pages:', 'hbd-theme' ) . '&after=</div>') ?>
					</div><!-- .entry-utility -->
					

                </div><!-- #post-<?php the_ID(); ?> -->           
    
            
 				<?php comments_template('', true); ?>

            </div><!-- #content -->
        </div><!-- #container -->
 
<?php get_sidebar(); ?>
<?php get_footer(); ?>