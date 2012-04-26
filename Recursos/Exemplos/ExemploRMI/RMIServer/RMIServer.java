/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package rmiserver;


import RMIInterface.RMIInterface;
import java.rmi.registry.LocateRegistry;
import java.rmi.registry.Registry;
import java.rmi.server.UnicastRemoteObject;

/**
 *
 * @author lassuncao
 */
public class RMIServer implements RMIInterface {
	
    public RMIServer() {}

    public String sayHello(String name) {
	    System.out.println("Method sayHello called with args:"+name);
	    return "Hello, "+name;
    }
	
    public Integer add(Integer a, Integer b) {
	    System.out.println("Method add called with args:"+a.intValue()+","+b.intValue());
        return new Integer(a.intValue()+b.intValue());
    }
    public static void main(String args[]) {
	
	try {
	    RMIServer obj = new RMIServer();
	    RMIInterface stub = (RMIInterface) UnicastRemoteObject.exportObject(obj, 0);

	    // Bind the remote object's stub in the registry
	    Registry registry = LocateRegistry.getRegistry();
	    registry.bind("Hello Server", stub);

	    System.err.println("Server ready");
	} catch (Exception e) {
	    System.err.println("Server exception: " + e.toString());
	    e.printStackTrace();
	}
    }
}

