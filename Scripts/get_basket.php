<?php
	require 'database.php';
	if (!isset($_POST['Id']))
	{
		echo 'Неверные данные!';
		exit;
	}
	echo json_encode(array('Goods' => get_basket($_POST['Id'])));
?>