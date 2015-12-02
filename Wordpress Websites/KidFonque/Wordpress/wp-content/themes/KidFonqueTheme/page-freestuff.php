<?php get_header(); ?>

<script>

    function statusChangeCallback(response) {
        console.log('statusChangeCallback');
        console.log(response);

        if (response.status === 'connected') {
            testAPI();
            //alert('connected');
        } else if (response.status === 'not_authorized') {
            //$("#btnFB").show();     
            //FB.login();
            document.getElementById('status').innerHTML = 'Please log into this app.';
        } else {
            //$("#btnFB").show();         
            //FB.login();
            document.getElementById('status').innerHTML = 'Please log into Facebook.';
            document.getElementById("FacebookLikeBox").style.display = 'none';
        }
    }

    function checkLoginState() {
        FB.getLoginStatus(function (response) {
            statusChangeCallback(response);
        });

    }

    window.fbAsyncInit = function () {
        FB.init({
            appId: '580929885357452', // App ID
            channelUrl: '//www.kidfonque.co.za/channel.html', // Channel File
            status: true, // check login status
            cookie: true, // enable cookies to allow the server to access the session
            xfbml: true,  // parse XFBML
            oauth: true
        });

        FB.getLoginStatus(function (response) {
            statusChangeCallback(response);
        });

    }

    function testAPI() {
        console.log('Welcome!  Fetching your information.... ');
        FB.api('/me', function (response) {
            console.log('Successful login for: ' + response.name);
            //document.getElementById('status').innerHTML = 'Thanks for logging in, ' + response.name + '!';
        });
        console.log('Fetching like information.... ');
        FB.api('/me/likes/160395350659987', function (response) {
            console.log(response.data);

            if (response.data.length > 0) {
                //alert('yes');

                //$('#FreeStuffPictures').show();
                 document.getElementById("FreeStuffPictures").style.display = 'block';
                 document.getElementById("FacebookLikeBox").style.display = 'none';
                 document.getElementById("FacebookLoginButton").style.display = 'none';
            } else {
                //alert('no');
                //$('#FreeStuffPictures').hide();
            }


        });
    }

</script>

<div id="outerContainer"> 
    <div id="container">
        <div id="containerPadding" class="clearfix">

            <div id="content">

                <div id="contentPage" class="NonHomePageContent">

                    <h2>FREE STUFF</h2>

                    <div id="FacebookLoginButton">

                        To get access to the free stuff, please can you login to facebook and approve my website, and then like my facebook page.<br/><br/>

                        <fb:login-button scope="user_likes" onlogin="checkLoginState();">
                        </fb:login-button>
                    </div>

                    <div id="status">
                    </div>
                    <br/>
                    <div id="FacebookLikeBox">
                    <div class="fb-like-box" data-href="https://www.facebook.com/kidfonque" data-colorscheme="light" data-show-faces="false" data-header="false" data-stream="false" data-show-border="false"></div>
                    </div>

                    <div id="FreeStuffPictures" style="display:  none">

                      
                        <?php the_post(); ?>
 
                        <div id="post-<?php the_ID(); ?>" <?php post_class(); ?>>
                    
                            <div class="entry-content">
                                <?php the_content(); ?>
                                <?php wp_link_pages('before=<div class="page-link">' . __( 'Pages:', 'your-theme' ) . '&after=</div>') ?>
                      
                            </div><!-- .entry-content -->

                        </div><!-- #post-<?php the_ID(); ?> -->           
 
                        <?php if ( get_post_custom_values('comments') ) comments_template() // Add a custom field with Name and Value of "comments" to enable comments on this page ?>            
 
                    </div><!-- #FreeStuffPictures -->

                </div><!-- #contentPage -->

            </div><!-- #content -->
            
	        <?php get_sidebar(); ?>

        </div><!-- #containerPadding -->
    </div><!-- #container -->
</div><!-- #outerContainer -->
 
<?php get_footer(); ?>
