<?php
    require 'database.php';
    if (!isset($_POST['Good']) or !isset($_POST['Change']))
    {
        echo 'Неверные данные!';
       exit;
    }

    if ($_POST['Change'] == 'Add')
        echo add_good($_POST['Good']);
    else if ($_POST['Change'] == 'Delete')
        echo delete_good($_POST['Good']);

?>