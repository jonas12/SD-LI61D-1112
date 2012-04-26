/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package RMIInterface;
import java.rmi.Remote;
import java.rmi.RemoteException;
/**
 *
 * @author lassuncao
 */



public interface RMIInterface extends Remote {
    
    String sayHello(String name) throws RemoteException;
    Integer add(Integer i, Integer j) throws RemoteException;
    
}
