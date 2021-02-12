<?php
	require 'database.php';
	    if (!isset($_POST['GoodId']) or !isset($_POST['UserId']))
   		 {
      		  echo 'Неверные данные!';
      		 exit;
    		}

	echo add_to_basket($_POST['GoodId'], $_POST['UserId']);
?>