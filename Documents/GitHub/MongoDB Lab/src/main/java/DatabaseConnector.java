import com.mongodb.client.MongoClient;
import com.mongodb.client.MongoClients;
import com.mongodb.client.MongoDatabase;

import java.nio.file.Files;
import java.nio.file.Paths;

public class DatabaseConnector {
    private static String URI_FILE_NAME = "database-uri.txt";
    private static String DATABASE_NAME = "dea_onderzoek_lab";

    public static MongoDatabase getDatabase() throws Exception {
        String uri = new String(Files.readAllBytes(Paths.get(ClassLoader.getSystemResource(URI_FILE_NAME).toURI())));
        MongoClient mongoClient = MongoClients.create(uri);
        return mongoClient.getDatabase(DATABASE_NAME);
    }
}
