<?php
$response['error'] = true;
$response['message'] = "Unknown Error";
$response['user'] = null;
include "db_connection.php";

if ($_SERVER["REQUEST_METHOD"] == "POST") {
    //Get data from post variables
    $username = $_POST['username'];
    $password = $_POST['password'];
    $remembered = $_POST['remembered'];

    //Get Record from database
    $query = "SELECT * FROM users WHERE username='{$username}'";
    $result = $conn->query($query);
    if($result->num_rows <=0){
        $response['error'] = true;
        $response['message'] = "Username or password incorrect.";
    }else{
        $user = $result->fetch_assoc();
        $password_hash = $user['password'];
        if($remembered == "true" || password_verify($password, $password_hash)){
            $response['error'] = false;
            $response['message'] = "User authenticated successfully.";
            $response['user'] = $user;
            //Get quests
            $query = "select quest_name, quest_description, quest_xp, quest_level, start_time, end_time, start_lat, start_long, end_lat, end_long from quests q, user_quests uq, users u where u.user_id = {$response['user']['user_id']} && u.user_id = uq.user_id && q.quest_id = uq.quest_id";
            $result = $conn->query($query);
            if($result->num_rows > 0){
                for($i = 0; $i < $result->num_rows; $i++){
                    $response['user']['quests'][$i] = $result->fetch_assoc();
                }
            }
            unset($response['user']['password']);
        }else{
            $response['error'] = true;
            $response['message'] = "Username or password incorrect.";
        }
    }
}else{
    $response['error'] = true;
    $response['message'] = "Invalid request method used. Please use POST";
}
echo json_encode($response);



