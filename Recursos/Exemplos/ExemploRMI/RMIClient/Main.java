/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package rmiclient;

import RMIInterface.RMIInterface;
import java.rmi.registry.LocateRegistry;
import java.rmi.registry.Registry;


/**
 *
 * @author lassuncao
 */
public class Main {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        // TODO code application logic here
       
	try {
	    Registry registry = LocateRegistry.getRegistry("localhost");
	    RMIInterface stub = (RMIInterface) registry.lookup("Hello Server");
            
	    
        System.out.println("ADD(3+2)="+stub.add( new Integer(2), new Integer(3)).intValue());
        String response = stub.sayHello("luis");
	    System.out.println("response: " + response);
	} catch (Exception e) {
	    System.err.println("Client exception: " + e.toString());
	    e.printStackTrace();
	}
    }
}


