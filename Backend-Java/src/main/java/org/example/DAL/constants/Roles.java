package org.example.DAL.constants;

import java.util.*;

public class Roles {
    public static String Admin="Admin";
    public static String Moder="Moder";
    public static String Editor="Editor";
    public static String Supporter="Supporter";
    public static String PermanentUser="Permanent User";
    public static String User="User";
    public static final List<String> All = Arrays.asList(
            Admin,
            Moder,
            Editor,
            Supporter,
            PermanentUser,
            User
    );

}
