#include <PololuMaestro.h>
#include <WiFi.h>
#include <HTTPClient.h>
#include <ArduinoJson.h>

MicroMaestro maestro(Serial2);
//SmartTerra:
//global server:
//String IP = "51.158.163.165";
//int smart_terra_device_id = 101;
//local SmartTerraAPI:
String SmartTerra_IP = "192.168.0.164:5000";
int smart_terra_device_id = 1002;
//---------------------------------------
//ROBOLab:
//local ROBOLabAPI:
String ROBOLab_IP = "192.168.0.164:5000";
int robo_arm_device_id = 2;

//const char* ssid = "Creative";
//const char* password = "Cre@tive";
const char* ssid = "UPCEA1369B";
const char* password = "uckKvpbZfzu3";

#define RXD2 16
#define TXD2 17

int max_angle = 180;
int number_of_elements = 0;
int angle_set_initial_pose [] = {5, 90, 4, 0, 2, 45, 3, 45, 1, 90, 0, 0};
  
int angle_set_amimation_move_teddy_bear [] = {
//zlap
  5, 0
, 3, 135
, 2, 15
, 0, 180
, 3, 170
, 0, 0
//podnies
, 3, 90
//przenies i odloz
, 5, 180
, 3, 170
, 0, 180
//wstan
, 3, 90
};

int angle_set_amimation_fill_water [] = {
//przesun
  5, 0
//zlap
, 3, 170
, 2, 80
, 0, 180
, 4, 90
, 0, 0
//nalej
, 5, 180
, 1, 0
, 1, 90
//odloz
, 5, 0
, 0, 180
, 4, 0
};

void setup() {
  Serial.begin(9600);
  Serial2.begin(9600, SERIAL_8N1, RXD2, TXD2);
  WiFi.begin(ssid, password);
  delay(1000);
  
  while (WiFi.status() != WL_CONNECTED) 
  {
    delay(1000);
    Serial.println("Connecting...");
  }
  Serial.println("Connected");
}

void loop() {
  if (WiFi.status() == WL_CONNECTED){
      //get_latest_device_property_id();
      Serial.println("initial:");
      //turn_on_servo(angle_set_initial_pose, 10, 0);
      delay(2000);
      //check_device_job_data(); 
   }
}

void print_servo_pos(int channel){
  uint16_t position = maestro.getPosition(channel);
  Serial.print("Channel: ");
  Serial.print(channel);
  Serial.print(", position: ");
  Serial.println(position);  
}

bool check_valid_angle_set_value(int channel, double angle){
  Serial.print("Channel: ");
  Serial.print(channel);
  Serial.print(", angle: ");
  Serial.println(angle);
  
  //check if the given angle is in the range 0-maxAngle, or if given channel is in the range 0-5
  if((channel<0)||(channel>5)||(angle<0)||(angle>max_angle)){
    Serial.println("Invalid size of angle or channel number.");
    return false;
  }
  return true;
}

void turn_on_servo(int angle_set [], int num_of_el, int pouring_time_millisec){
  
  //check if size of angleSet is even number
  if(num_of_el%2==1){
    Serial.println("Invalid size of angleSet.");
    return;
  }
  
  for(int i=0; i<num_of_el; i+=2){
    bool is_angle_value_valid = check_valid_angle_set_value(angle_set[i], angle_set[i+1]);
    if(!is_angle_value_valid){
      return;
    }
    //Serial.print("i: ");
    //Serial.println(i);
    
    if(pouring_time_millisec!=0 && i==16){
    }
    int converted_angle = converted_angle = map(angle_set[i+1], 0, max_angle, 2000, 10000);
    //Serial.print("converted_angle: ");
    //Serial.println(converted_angle);
    maestro.setTarget(angle_set[i], converted_angle);
    if(pouring_time_millisec!=0 && i==16){
      delay(pouring_time_millisec);
    }
    else{
      delay(1000);
    }
    
    send_angle_data_by_prop_id(angle_set[i]+1, angle_set[i+1]);
  }
}

JsonArray read_as_json_array(DynamicJsonDocument doc){
  JsonArray job_body_array = doc["body"].as<JsonArray>();
  Serial.print("job_body_array: ");
  Serial.println((String)job_body_array);
  
  for(JsonVariant v : job_body_array) {
       Serial.println(v.as<int>());
  }
  Serial.print("job_body_array size: ");
  Serial.println(job_body_array.size());
}

int* convert_to_int_array(String job_body_string){
  
  int index_of_coma = 1;
  static int angle_set_int [12] = {};
  number_of_elements = 0;
  int j=0;
  for(int i=1; i<=job_body_string.length(); i++){
    if(job_body_string[i] == ','&&index_of_coma==1){
      Serial.println("-----");
      Serial.println("if 1");
      Serial.println(i);
      Serial.println(j);
      
      String val = job_body_string.substring(index_of_coma,i);
      int v = val.toInt();
      angle_set_int[j] = v;
      index_of_coma=i;
      j++;
      Serial.println("values");
      Serial.println(val);
      Serial.println(v);
      Serial.println((String)angle_set_int[j]);
    }
    else if(job_body_string[i] == ','){
      Serial.println("-----");
      Serial.println("if 2");
      Serial.println(i);
      Serial.println(j);
      
      String val = job_body_string.substring(index_of_coma+1,i);
      int v = val.toInt();
      angle_set_int[j]= v;
      index_of_coma=i;
      j++;
      Serial.println("values");
      Serial.println(val);
      Serial.println(v);
      Serial.println((String)angle_set_int[j]);
    }
    else if(i == job_body_string.length()){
      Serial.println("-----");
      Serial.println("if 3");
      Serial.println(i);
      Serial.println(j);
      
      String val = job_body_string.substring(index_of_coma+1,i-1);
      int v = val.toInt();
      angle_set_int[j] = v;
      index_of_coma=i;
      j++;
      Serial.println("values");
      Serial.println(val);
      Serial.println(v);
      Serial.println((String)angle_set_int[j]);//dlaczego tutaj nie wypisuja sie wartosci mimo ze pozniej sa ok 
    }
    number_of_elements = j+1;
  }
  return angle_set_int;
}

void run_any_sequence(String sequence_of_angles){
  int index_of_open_bracket = -1, index_of_close_bracket = -1;
  Serial.println("run_any_sequence: ");
  
  for(int i=0; i<sequence_of_angles.length(); i++){
    
    if(sequence_of_angles[i] == '['){
      Serial.print("index_of [: ");
      Serial.println(i);
      index_of_open_bracket = i;
    }
    
    if(sequence_of_angles[i] == ']'){
      Serial.print("index_of ]: ");
      Serial.println(i);
      index_of_close_bracket = i;
    }
    
    if(index_of_open_bracket != -1 && index_of_close_bracket != -1){
      String set_of_angles = sequence_of_angles.substring(index_of_open_bracket, index_of_close_bracket+1);
      //int* int_array = convert_to_int_array(set_of_angles);
      Serial.println("final set_of_angles");
      Serial.println(set_of_angles);
      index_of_open_bracket = -1; index_of_close_bracket = -1;
      //turn_on_servo(int_array, number_of_elements, 0);
    }
  }
}

//roboLab send data:
void send_angle_data_by_prop_id(int prop_id, int value){
  
  String data = "{\"propertyId\":" + String(prop_id) + " , \"val\": \"" + String(value) + "\"}";
  
  HTTPClient http;    
  http.begin("http://"+ String(ROBOLab_IP)+"/api/devices/"+ String(robo_arm_device_id)+"/add-values-by-property-id");
  Serial.print("[send_angle_data_by_prop_id] request url: ");
  Serial.println("http://"+ String(ROBOLab_IP)+"/api/devices/"+ String(robo_arm_device_id)+"/add-values-by-property-id");
  //specify content-type header
  http.addHeader("Content-Type", "application/json");  
  
  int httpCode = http.POST(data);
  Serial.print("[send_angle_data_by_prop_id] data: ");
  Serial.println(data);   
  String payload = http.getString(); 

  Serial.print("[send_angle_data_by_prop_id] payload: ");
  Serial.println(payload);

  http.end();
}

String BoolToString(bool b)
{
  return b ? (String)"true" : (String)"false";
}

//roboLab change job done value:
void set_job_done_property(int dj_id, bool done_value){
  HTTPClient http;
  http.begin("http://"+ String(ROBOLab_IP)+"/api/device-jobs/"+ String(dj_id)+"/done-flag-value");
  http.addHeader("Content-Type", "application/json-patch+json");
  int httpCode = http.PATCH("{\"done\": "+ BoolToString(done_value) + " }");
  
  Serial.print("[set_job_done_property] http_code: ");
  Serial.println(httpCode);
  Serial.print("[set_job_done_property] reques_body: ");
  Serial.println("{\"done\": "+ BoolToString(done_value) + " }");
  http.end();
}

//terrarium prop:
void get_latest_device_property_id(){
  HTTPClient http;
  http.begin("http://"+ String(SmartTerra_IP)+"/api/Devices/"+ String(smart_terra_device_id)+"/latestDeviceProperties");
  int httpCode = http.GET();
  String http_value = http.getString();
  
  Serial.print("httpCode: "); Serial.println(httpCode);

  if (httpCode > 0) 
    {
      const size_t bufferSize = JSON_OBJECT_SIZE(3) + JSON_OBJECT_SIZE(4) + JSON_OBJECT_SIZE(5) + JSON_OBJECT_SIZE(6) + 700;
      DynamicJsonDocument doc(bufferSize);
      
      DeserializationError error = deserializeJson(doc, http_value);

      // Test if parsing succeeds
      if (error) {
        Serial.print(F("deserializeJson() failed: "));
        Serial.println(error.f_str());
        http.end();
        return;
      }
      
      int dev_prop_id = doc["id"];
      bool isLiquidLevelSufficient = doc["isLiquidLevelSufficient"];
      //const char* isLiquidLevelSufficient = doc["isLiquidLevelSufficient"];
      Serial.print("[get_latest_device_property_id] dev_prop_id: "); Serial.println(dev_prop_id);
      Serial.println("http_value: "); Serial.println(http_value);
      Serial.print("isLiquidLevelSufficient: "); Serial.println(isLiquidLevelSufficient);
  }
  
  http.end();
}

//robo arm job:
void check_device_job_data(){
  Serial.println("[check_device_job_data]");
  
  HTTPClient http;
  http.begin("http://"+ String(ROBOLab_IP)+"/api/device-jobs/device/"+ String(robo_arm_device_id)+"/false-done-flag");
  int httpCode = http.GET();
  
  String http_value = http.getString();
  Serial.println("httpCode:");
  Serial.println(httpCode);
  
  if (httpCode > 0)
    {  
      Serial.println("raw string:");
      Serial.println(http_value);
      
      const size_t bufferSize = JSON_OBJECT_SIZE(3) + JSON_OBJECT_SIZE(4) + JSON_OBJECT_SIZE(5) + JSON_OBJECT_SIZE(6) + 700;
      DynamicJsonDocument doc(bufferSize);
      
      DeserializationError error = deserializeJson(doc, http_value);

      if(!doc.containsKey("job"))
      {
        Serial.println("There is no jobs to do from API.");
        http.end();
        return;
      }
      
      // Test if parsing succeeds
      if (error) {
        Serial.print(F("deserializeJson() failed: "));
        Serial.println(error.f_str());
        http.end();
        return;
      }
          
      int device_job_id= doc["id"];
      const char* job_type = doc["job"]["deviceTypeName"];
      const char* job_name = doc["job"]["name"];
      const char* job_body = doc["body"];
      
      Serial.print("job_type: ");
      Serial.println((String)job_type);
      Serial.print("job_name: ");
      Serial.println((String)job_name);
      Serial.print("job_body: ");
      Serial.println((String)job_body);

      String job_body_string = (String)job_body;
      
      if(!doc.containsKey("job"))
      {
        Serial.println("There is no jobs to do from API.");
        http.end();
        return;
      }
      
      if((String)job_type=="ROBOTIC ARM"){
        
        if((String)job_name=="MoveTeddyBear"){
            //read_as_json_array(doc);<--- metoda Daniela
            
            int* job_angle;
            job_angle = convert_to_int_array(job_body_string);//<--- bardzo toporna metoda Oli
            for(int i=0; i<2; i++){
              Serial.print("job_angle: ");
              Serial.println(job_angle[i]);
            }
            
            angle_set_amimation_move_teddy_bear[1] = job_angle[0];
            angle_set_amimation_move_teddy_bear[15] = job_angle[1];
            
            Serial.println("Animation job: MoveTeddyBear");
            turn_on_servo(angle_set_amimation_move_teddy_bear, 22, 0);
            delay(2000);

            bool job_done_value = true;
            set_job_done_property(device_job_id, job_done_value);
          }

          if((String)job_name=="FillCubeWithWater"){
            int pouring_time_millisec = doc["body"];
            Serial.print("pouring_time_millisec: ");
            Serial.println(pouring_time_millisec);
              
            Serial.println("Animation job: FillCubeWithWater");
            turn_on_servo(angle_set_amimation_fill_water, 24, pouring_time_millisec);
            delay(2000);

            bool job_done_value = true;
            set_job_done_property(device_job_id, job_done_value);
          }

          if((String)job_name=="RunAnySequence"){
            String sequence_of_angles = doc["body"];
            Serial.print("sequence_of_angles: ");
            Serial.println(sequence_of_angles);
              
            Serial.println("Animation job: RunAnySequence");
            run_any_sequence(sequence_of_angles);
            delay(2000);

            bool job_done_value = true;
            set_job_done_property(device_job_id, job_done_value);
          }
      }
    }
  http.end();
}
