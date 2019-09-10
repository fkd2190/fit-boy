<?php
use PHPUnit\Framework\TestCase;

class RegisterUserTests extends TestCase
{
    protected function setUp(): void
    {
        //Add dummy data to database to test
        include('db_connection.php');
        $hash = password_hash('password', PASSWORD_DEFAULT);
        $query = "INSERT INTO users (username, password, email)
                VALUES ('joebloggs', '{$hash}', 'someone@example.com')";
        $conn->query($query);
    }

    public function test_register_valid_user()
    {
        //Attempts to add a user with valid information to the database.

        //Setup local variables for the register.php script to use.
        $_SERVER["REQUEST_METHOD"] = "POST";
        $_POST['username'] = "TestPerson";
        $_POST['password'] = "password";
        $_POST['email'] = "testPerson@example.com";

        //Run file
        ob_start();
        include('register.php');
        $response = json_decode(ob_get_clean(), true);
        $this->assertFalse($response['error']);
    }

    public function test_existing_username()
    {
        //Setup local variables for the register.php script to use.
        $_SERVER["REQUEST_METHOD"] = "POST";
        $_POST['username'] = "joebloggs";
        $_POST['password'] = "password";
        $_POST['email'] = "testPerson@example.com";

        //Run file
        ob_start();
        include('register.php');
        $response = json_decode(ob_get_clean(), true);
        $this->assertTrue($response['error']);
    }

    public function test_existing_email()
    {
        //Setup local variables for the register.php script to use.
        $_SERVER["REQUEST_METHOD"] = "POST";
        $_POST['username'] = "TestPerson";
        $_POST['password'] = "password";
        $_POST['email'] = "someone@example.com";

        //Run file
        ob_start();
        include('register.php');
        $response = json_decode(ob_get_clean(), true);
        $this->assertTrue($response['error']);
    }

    public function test_invalid_email_format()
    {
        //Setup local variables for the register.php script to use.
        $_SERVER["REQUEST_METHOD"] = "POST";
        $_POST['username'] = "TestPerson";
        $_POST['password'] = "password";
        $_POST['email'] = "testPerson@example";

        //Run file
        ob_start();
        include('register.php');
        $response = json_decode(ob_get_clean(), true);
        $this->assertTrue($response['error']);
    }

    public function test_empty_password()
    {
        //Setup local variables for the register.php script to use.
        $_SERVER["REQUEST_METHOD"] = "POST";
        $_POST['username'] = "TestPerson";
        $_POST['password'] = "";
        $_POST['email'] = "testPerson@example.com";

        //Run file
        ob_start();
        include('register.php');
        $response = json_decode(ob_get_clean(), true);
        $this->assertTrue($response['error']);
    }

    public function test_empty_username()
    {
        //Setup local variables for the register.php script to use.
        $_SERVER["REQUEST_METHOD"] = "POST";
        $_POST['username'] = "";
        $_POST['password'] = "password";
        $_POST['email'] = "testPerson@example.com";

        //Run file
        ob_start();
        include('register.php');
        $response = json_decode(ob_get_clean(), true);
        $this->assertTrue($response['error']);
    }

    public function test_invalid_http_method()
    {
        //Setup local variables for the register.php script to use.
        $_SERVER["REQUEST_METHOD"] = "GET";
        $_POST['username'] = "TestPerson";
        $_POST['password'] = "password";
        $_POST['email'] = "testPerson@example.com";

        //Run file
        ob_start();
        include('register.php');
        $response = json_decode(ob_get_clean(), true);
        $this->assertTrue($response['error']);
    }

    protected function tearDown(): void
    {
        include('db_connection.php');
        $query = "DELETE FROM users WHERE username = 'joebloggs' OR username = 'TestPerson'";
        $conn->query($query);
    }
}
