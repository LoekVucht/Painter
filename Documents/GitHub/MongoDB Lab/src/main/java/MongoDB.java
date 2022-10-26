import com.mongodb.client.*;
import com.mongodb.client.model.Projections;
import com.mongodb.client.model.UpdateOptions;
import com.mongodb.client.model.Updates;
import org.bson.BsonDocument;
import org.bson.BsonString;
import org.bson.Document;
import org.bson.conversions.Bson;

import java.util.Scanner;

public class MongoDB {
    public static void main(String[] args) throws Exception {
        MongoDatabase database = DatabaseConnector.getDatabase();
        MongoCollection<Document> usersCollection = database.getCollection("users");
        Scanner scanner = new Scanner(System.in);
        while (true) {
            System.out.println("Selecteer een actie door een nummer te selecteren:");
            System.out.println("1 - Gebruiker toevoegen");
            System.out.println("2 - Gebruikersnaam wijzigen");
            System.out.println("3 - Gebruiker verwijderen");
            System.out.println("4 - Huidige gebruikers tonen");
            String choice = scanner.nextLine();
            switch (choice) {
                case "1" -> addUser(usersCollection, scanner);
                case "2" -> updateUsername(usersCollection, scanner);
                case "4" -> showUserDocuments(usersCollection);
            }
        }
    }

    private static void addUser(MongoCollection<Document> usersCollection, Scanner scanner) {
        while (true) {
            System.out.println("Voer een gebruikersnaam in.");
            String enteredUsername = scanner.nextLine();
            if (usernameExists(usersCollection, enteredUsername)) {
                System.out.println("Deze gebruikersnaam bestaat al.");
            } else {
                addUser(usersCollection, enteredUsername);
                break;
            }
        }
    }

    private static void updateUsername(MongoCollection<Document> usersCollection, Scanner scanner) {
        String enteredCurrentUsername;
        while (true) {
            System.out.println("Voer de gebruikersnaam die je wilt wijzigen in.");
            enteredCurrentUsername = scanner.nextLine();
            if (!usernameExists(usersCollection, enteredCurrentUsername)) {
                System.out.println("Deze gebruikersnaam bestaat niet.");
            }
            else break;
        }
        while (true) {
            System.out.println("Voer een nieuwe gebruikersnaam in.");
            String enteredNewUsername = scanner.nextLine();
            if (!usernameExists(usersCollection, enteredNewUsername)) {
                updateUsername(usersCollection, enteredCurrentUsername, enteredNewUsername);
                System.out.println("De gebruikersnaam \"" + enteredCurrentUsername +
                        "\" is veranderd in \"" + enteredNewUsername + "\".");
                break;
            }
            else {
                System.out.println("Deze gebruikersnaam bestaat al.");
            }
        }
    }

    private static boolean usernameExists(MongoCollection<Document> usersCollection, String username) {
        return usersCollection.countDocuments(new BsonDocument("username", new BsonString(username))) > 0;
    }

    private static void addUser(MongoCollection<Document> usersCollection, String username) {
        usersCollection.insertOne(new Document("username", username));
    }

    private static void updateUsername(MongoCollection<Document> usersCollection, String currentUsername, String newUsername) {
        Document query = new Document().append("username", currentUsername);
        Bson update = Updates.set("username", newUsername);
        usersCollection.updateOne(query, update, new UpdateOptions().upsert(true));
    }

    private static void showUserDocuments(MongoCollection<Document> userCollection) {
        System.out.println("De collectie \"username\" bevat de volgende documenten:");
        MongoCursor<Document> cursor = userCollection.find().projection(Projections.include("username")).iterator();
        while (cursor.hasNext()) {
            System.out.println(cursor.next());
        }
    }
}