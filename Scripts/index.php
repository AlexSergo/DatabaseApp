<?php
	require 'database.php';
	echo json_encode(array('Goods' => get_goods()));
?>