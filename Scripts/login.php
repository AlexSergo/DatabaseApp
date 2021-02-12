<?php
	require 'database.php';
	$login = $_POST['Login'];
	$password = $_POST['Password'];
	if (!isset($login) or !isset($password)){
		echo 'неверные данные!'; 
		exit;
	}
	echo json_encode(check_user($login, $password));
?>