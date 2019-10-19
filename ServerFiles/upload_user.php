<?php
$response['error'] = true;
$response['message'] = "Unknown error";

include "db_connection.php";

if($_SERVER['REQUEST_METHOD'] == "POST"){
    $response['error'] = false;
    //Variables
	$username = $_POST['username'];
    $new_email = $_POST['new_email'];
    $xp = $_POST['xp'];
    $level = $_POST['level'];
    $old_password = $new_password = "";
    if(isset($_POST['old_password'])){
        $old_password = $_POST['old_password'];
    }
    if(isset($_POST['new_password'])){
        $new_password = $_POST['new_password'];
    }

    //Check if user exists
    $query = "SELECT * FROM users WHERE username = '{$username}'";
    $result = $conn->query($query);
    if($result->num_rows > 0){
		//if user is exists, then update information
//		$query = "UPDATE users SET email = '{$new_email}', xp = {$xp}, level = {$level} WHERE username = '{$username}'";
        $query = "UPDATE users SET ";

        if(!empty($old_password)&& !empty($new_password)){
            //Check if old password matches
            $user = $result->fetch_assoc();
            $old_password_hash = $user['password'];
            $temp_password_hash = $user['temp_password'];
            if(password_verify($old_password, $old_password_hash) || password_verify($old_password, $temp_password_hash)){
                $hashed_password = password_hash($new_password, PASSWORD_DEFAULT);
                $query .= "password = '{$hashed_password}', ";
            }else{
                $response['error'] = true;
                $response['message'] = "Passwords do not match";
            }
            $conn->query("UPDATE users SET temp_password = '' WHERE username = '{$username}'");
        }
        if(!empty($new_email)){
            if(filter_var($new_email, FILTER_VALIDATE_EMAIL)) {
                $query .= "email = '{$new_email}',";
            }else{
                $response['error'] = true;
                $response['message'] = "Email not in valid format";
            }
        }

        $query .= "xp = {$xp}, level = {$level} WHERE username = '{$username}'";
        if(!$response['error']) {
            $conn->query($query);
            if (!$conn->error) {
                $response['error'] = false;
                $response['message'] = "User updated successfully." . $query;
            } else {
                $response['error'] = true;
                $response['message'] = "Error updating user details: " . $conn->error;
            }
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
