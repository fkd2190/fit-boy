<?php


use PHPUnit\Framework\TestCase;

class DeleteUserTests extends TestCase
{
    public function test_delete_existing_user()
    {
        //Add dummy user to database
        include "db_connection.php";
        $conn->query("INSERT INTO users (username) VALUES ('3135035_fitboy')");

        //Setup local variables for the delete_user.php script to use.
        $_SERVER["REQUEST_METHOD"] = "POST";
        $_POST['username'] = "3135035_fitboy";

        ob_start();
        include('delete_user.php');
        $response = json_decode(ob_get_clean(), true);

        //Remove dummy user
        $conn->query("DELETE FROM users WHERE username = '3135035_fitboy'");

        $this->assertFalse($response['error']);
    }

    public function test_delete_user_invalid_request_method()
    {
        $_SERVER['REQUEST_METHOD'] = "GET";
        $_POST['username'] = '3135035_fitboy';

        ob_start();
        include('delete_user.php');
        $response = json_decode(ob_get_clean(), true);
        $this->assertTrue($response['error']);
    }
}
