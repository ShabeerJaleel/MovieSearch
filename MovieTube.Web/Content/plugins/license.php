<?php
header('Content-Type: application/x-shockwave-flash');
header('Cache-Control: no-cache');
echo file_get_contents("license.swf");
?>