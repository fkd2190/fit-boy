<?php
$response['error'] = true;
$response['message'] = "Unknown Error";
$response['user'] = null;
include "db_connection.php";

if ($_SERVER["REQUEST_METHOD"] == "POST") {
    //Get data from post variables
    $email = $_POST['email'];

    //Check if user exists
    $query = "SELECT * FROM users WHERE email = '{$email}'";
    $result = $conn->query($query);
    if($result->num_rows == 1){
        $username = $result->fetch_assoc()['username'];
        //Generate new temporary password
        $chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%&*_";
        $password = substr( str_shuffle( $chars ), 0, 8 );
        $hashed_password = password_hash($password, PASSWORD_DEFAULT);

        $query = "UPDATE users SET temp_password = '{$hashed_password}' WHERE email = '{$email}'";
        $result = $conn->query($query);
        if(!$conn->error){
            $message = "Hello {$username},\n\n";
            $message .= "Your password has been reset. Please log in to the app using the temporary password below.";
            $message .= "\nIf you did not request a password reset, your original password will still work.\n\n";
            $message .= "Temporary Password: " . $password;
            $message .= "\n\nOnce you have logged in, please update your password via your profile.";
            if(mail($email, "Fit-Boy password reset", $message, "From: Fit-Boy <noreply@fitboy.tk>" . "\r\n")){
                $response['error'] = false;
                $response['message'] = "Email sent successfully.";
            }else{
                $response['error'] = true;
                $response['message'] = "Error sending reset email.";
            }
        }else{
            $response['error'] = true;
            $response['message'] = "Database error.";
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



