
 
    <div id="footer">
        
        <img id="footerImage" src="/wp-content/themes/KidFonqueTheme/images/kidfonque-footer-<?php echo $GLOBALS['HeaderImageNumber']?>.jpg" />

            <div id="footer-sidebar">
            <?php
            if(is_active_sidebar('footer_sidebar')){
                dynamic_sidebar('footer_sidebar');
            }
            ?>
            </div>    
    </div><!-- #footer -->
</div><!-- #wrapper -->
<?php wp_footer(); ?>
</body>
</html>