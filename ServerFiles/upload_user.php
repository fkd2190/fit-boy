<?php
$response['error'] = true;
$response['message'] = "Unknown error";

include "db_connection.php";

if($_SERVER['REQUEST_METHOD'] == "POST"){
    //Variables
	$username = $_POST['username'];
    $new_email = $_POST['new_email'];
    $xp = $_POST['xp'];
    $level = $_POST['level'];

    //Check if user exists
    $query = "SELECT * FROM users WHERE username = '{$username}'";
    $result = $conn->query($query);
    if($result->num_rows > 0){
		//if user is exists, then update information
		$query = "UPDATE users SET email = '{$new_email}', xp = {$xp}, level = {$level} WHERE username = '{$username}'";
		$conn->query($query);
		if(!$conn->error){
			$response['error']= false;
			$response['message'] = "User updated successfully.";
		}else{
			$response['error'] = true;
			$response['message'] = "Error updating user details: " . $conn->error;
		}
        
    }else{
        $response['error'] = true;
        $response['message'] = "Username not found.";
    }
}else{
    $response['error'] = true;
    $response['message'] = "Invalid request method used. Please use POST";
}

echo json_encode($response);
