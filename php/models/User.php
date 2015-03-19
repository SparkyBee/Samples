<?php
/**
 * Created by JetBrains PhpStorm.
 * User: Barrett
 * Date: 4/29/12
 * Time: 4:15 PM
 * To change this template use File | Settings | File Templates.
 */
class User extends  ActiveRecord\Model
{

    static $has_many = array(
        array('posts'),
        array('comments')
    );

    /**
     * var $password = FALSE;

    function before_save() {

        if ($this->password)
            $this->password_hash = $this->hash_password($this->password);

    }
    */

    function set_password($plaintext){

        $this->password_hash = $this->hash_password($plaintext);
    }

    private function hash_password($password){

        $salt = bin2hex(mcrypt_create_iv(32,MCRYPT_DEV_URANDOM));
        $hash = hash('sha256', $salt . $password);
        return $salt .$hash;
    }

    private  function validate_password($password){

        $salt = substr($this->password_hash, 0, 64);
        $hash = substr($this->password_hash, 64, 64);

        $valid_hash = hash('sha256', $salt . $password);

        return $valid_hash == $hash;

    }

    public static function validate_login ($username, $password){

        $user = User::find_by_username($username);

        if($user && $user->validate_password($password))
        {
            User::login($user->id);
            return $user;
        }
        else
            return FALSE;

    }

    public static function login($user_id){

        $CI =& get_instance();
        $CI->session->set_userdata('user_id',$user_id);
    }

    public static function logout(){

        $CI =& get_instance();
        $CI->session->sess_destroy();
    }



}
