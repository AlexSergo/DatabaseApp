<?php
	require 'rb-mysql.php'; 
	
	R::setup('mysql:host=localhost;dbname=alexsergol',
        'alexsergol', 'printHello1');
$mysqli = new mysqli("localhost", "alexsergol", "printHello1", "alexsergol");
		mysqli_set_charset( 'utf-8', $mysqli);
		
	if (!R::testConnection())
	{
		echo 'Не удалось подключиться!';
		exit;
	}	
	
	function add_good($name)
	{
		$good = R::dispense('good');
		$good->name = $name;
		R::store($good);
		return "Товар $good->name успешно добавлен!";
	}

	function delete_good($name)
	{
		R::exec('DELETE FROM `good` WHERE `name` = ?', [$name]);
		return "Товар $name успешно удален!";
	}


	function get_goods(){
		$idArray = array();
		$goods = R::getAll('SELECT * FROM good');
		$array = array();
		foreach($goods as $good)
			array_push($array, $good);	
		return $array;
	}

	function check_user($login, $password){
		$password = md5($password);
		$countOfUsers = R::getCell('SELECT COUNT(`id`) AS `count` FROM `user` WHERE `login` = ?',
		[$login]);
		if ($countOfUsers == 0){
			$new_user = R::dispense('user');
			$new_user->login = $login;
			$new_user->password = $password;
			R::store($new_user);
			return $new_user;
		}
		else
		{
			$user = R::getRow('SELECT * FROM `user` WHERE login = ?AND `password` = ?  LIMIT 1',
			[$login, $password]);
			return $user;
		}
	}

	function add_to_basket($good_id, $user_id){
		R::exec('INSERT INTO basket (user_id, good_id) VALUES (?, ?)', [$user_id, $good_id]);
		return 'Ok';
	}

	function get_basket($id){
		$basket = R::getAll('SELECT good_id FROM basket WHERE user_id = ?', [$id]);
		$array_id = array();
		foreach ($basket as $item_id){
			array_push($array_id, $item_id['good_id']);
		}
		$goods = R::loadAll('good', $array_id);
		$goods_array = array();
		foreach ($array_id as $good_id)
			 array_push($goods_array, $goods[$good_id]);
		return $goods_array;
	}
?>