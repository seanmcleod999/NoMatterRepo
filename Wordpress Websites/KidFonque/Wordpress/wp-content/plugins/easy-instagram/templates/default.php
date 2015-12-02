<?php
if ( ! class_exists( 'Easy_Instagram_Utils' ) ) :
	_e( 'Please install the Easy Instagram plugin.', 'Easy_Instagram' );
else :
	$ei_utils = new Easy_Instagram_Utils();
	$kses_author = array( 
		'a' => array( 'href' => array(), 'title' => array(), 'target' => array() )
	);
	
	
?>
<div class="easy-instagram-container default">

    <h2>Instagram</h2>

<?php foreach ( $easy_instagram_elements as $element ) : ?>
	<div class='easy-instagram-thumbnail-wrapper' >
	<?php echo $ei_utils->get_thumbnail_html( $element ); ?>

	
	</div>
<?php endforeach; ?>
</div>
<?php endif; ?>