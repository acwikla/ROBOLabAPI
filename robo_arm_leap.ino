#include <PololuMaestro.h>
#include <WiFi.h>
#include <HTTPClient.h>
#include <ArduinoJson.h>
MicroMaestro maestro(Serial2);
//https://pololu.github.io/maestro-arduino/class_maestro.html

#define RXD2 16
#define TXD2 17

String ROBOLab_adress = "http://51.158.163.165/api/";
int robo_arm_device_id = 2;
int dev_job_id = 0;

//const char* ssid = "Creative";
//const char* password = "Cre@tive";
const char* ssid = "UPCEA1369B";
const char* password = "uckKvpbZfzu3";

int max_angle = 180;
int number_of_elements = 0;
int angle_set_initial_pose [] = {5, 90, 4, 120, 3, 120, 2, 15, 1, 90, 0, 90};
bool print_info_flag=false;
void turn_on_servo_with_basic_angle_set(int angle_set [], int num_of_el, int pouring_time_millisec=0){
  
  Serial.println("turn_on_servo_with_basic_angle_set");
  //check if size of angleSet is even number
  if(num_of_el%2==1){
  }
  
  for(int i=0; i<num_of_el; i+=2){
    bool is_angle_value_valid = check_valid_angle_set_value(angle_set[i], angle_set[i+1]);
    if(!is_angle_value_valid){
      return;
    }
    Serial.println("chanel: "+String(angle_set[i])+" angle: "+String(angle_set[i+1]));
    int converted_angle = converted_angle = map(angle_set[i+1], 0, max_angle, 2000, 10000);
    maestro.setTarget(angle_set[i], converted_angle);
    //delay(1000);
  }
}

bool check_valid_angle_set_value(int channel, int angle){
  //check if the given angle is in the range 0-maxAngle, or if given channel is in the range 0-5
  if((channel<0)||(channel>5)||(angle<0)||(angle>max_angle)){
    Serial.println("Invalid size of angle or channel number.");
    return false;
  }
  return true;
}

void turn_on_servo_with_angle_differences(int channel_table [],int angle_differences_table [], int lenght){
  
  Serial.println("turn_on_servo_with_angle_differences");
  
  for(int i=0; i<lenght; i++){
    
    //int channel_position = map(maestro.getPosition(channel_table[i]), 2000, 10000, 0, max_angle);
    int channel_position = maestro.getPosition(channel_table[i]);
    
    int converted_to_radious_channel_position = map(channel_position, 2000, 10000, 0, max_angle);
    
    int difference_angle = map(angle_differences_table[i], 0, max_angle, 2000, 10000);

    int full_angle_radious=converted_to_radious_channel_position+angle_differences_table[i];

    Serial.println("chanel: "+String(channel_table[i])+" channel_position_maestro: "+String(channel_position));
    Serial.println("channel_position_radious: "+String(converted_to_radious_channel_position));
    Serial.println("difference_angle_maestro: "+String(map(angle_differences_table[i], 0, max_angle, 2000, 10000)));
    Serial.println("difference_angle_radious: "+String(angle_differences_table[i]));
    Serial.println("full_angle_radious: "+String(full_angle_radious));
    
    bool is_angle_value_valid = check_valid_angle_set_value(channel_table[i], full_angle_radious);
    if(!is_angle_value_valid){
      return;
    }
    
    //finalnie dobra wartosc:
    int full_angle_radious_maestro = map(full_angle_radious, 0, max_angle, 2000, 10000);
    Serial.println("full_angle_radious_maestro: "+String(full_angle_radious_maestro));
    maestro.setTarget(channel_table[i], full_angle_radious_maestro);
    
    //delay(1000);
  }
}

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
  Serial.println("initial:");
  turn_on_servo_with_basic_angle_set(angle_set_initial_pose, sizeof(angle_set_initial_pose)/sizeof(int));
}

void loop() {
  // put your main code here, to run repeatedly:
  
  //String job_body ="channels:[0,1,2,4,3,5];  angleDifferences: [10,20,-10,-20,5,10]";
  //run_any_difference_sequence(job_body);
  
  if (WiFi.status() == WL_CONNECTED){
      
      check_device_job_data(); 
   }
}

void print_info(String info){
  if(print_info_flag){
    Serial.println(info);
  }
}

void run_any_sequence(String sequence_of_angles){
  Serial.println("[run_any_sequence]");
  
  int sequence [2]={};
  int index_of_coma= sequence_of_angles.indexOf(',');
  int channel = sequence_of_angles.substring(0,index_of_coma).toInt();
  int angle = sequence_of_angles.substring(index_of_coma+1).toInt();
  sequence[0]=channel-1;sequence[1]=angle;
  turn_on_servo_with_basic_angle_set(sequence, 2);
}

void run_any_difference_sequence(String sequence_of_angles){
  Serial.println("[run_any_difference_sequence]");
  print_info("job_body(sequence_of_angles): "+sequence_of_angles);
  
  sequence_of_angles.trim();
  int channel_table_index= sequence_of_angles.lastIndexOf("channels");
  int angle_differences_table_index= sequence_of_angles.lastIndexOf("angleDifferences");
  String channel_table_string = sequence_of_angles.substring(channel_table_index+9, angle_differences_table_index);
  String angle_differences_table_string = sequence_of_angles.substring(angle_differences_table_index+17); 
  
  print_info("channel_table_string: "+channel_table_string);
  print_info("angle_differences_table_string: "+angle_differences_table_string);

  int channel_table_length = 1;
  for(int i=0; i<channel_table_string.length();i++){
    if(channel_table_string[i]==','){
      channel_table_length++;
    }
  }
  
  int angle_differences_table_length = 1;
  for(int i=0; i<angle_differences_table_string.length();i++){
    if(angle_differences_table_string[i]==','){
      angle_differences_table_length++;
    }
  }

  //check if size of angleSet is even number
  if(channel_table_length != angle_differences_table_length){
    Serial.println("Tables length must be equal.");
    return;
  }
  
  int channel_table [channel_table_length];
  channel_table_string.trim();
  int index_of_coma =0, value_index=0;

  //https://arduino.stackexchange.com/questions/1013/how-do-i-split-an-incoming-string
  
  for(int i=0; i<channel_table_string.length();i++){
    if(channel_table_string[i]==','){
      int val = channel_table_string.substring(index_of_coma+1, i).toInt();
      channel_table[value_index]= val;
      print_info("val: "+(String)val);
      print_info("channel_table["+(String)i+"]: "+(String)channel_table[value_index]);
      value_index++;
      index_of_coma=i;
    }
    if(i+1==channel_table_string.length()){
      int val = channel_table_string.substring(index_of_coma+1).toInt();
      channel_table[value_index]= val;
      print_info("val: "+(String)val);
      print_info("channel_table["+(String)i+"]: "+(String)channel_table[value_index]);
      value_index++;
      index_of_coma=i;
    }
  }

  index_of_coma =0, value_index=0;
  int angle_differences_table [channel_table_length];
  angle_differences_table_string.trim();

  for(int i=0; i<angle_differences_table_string.length();i++){
    if(angle_differences_table_string[i]==','){
      int val = angle_differences_table_string.substring(index_of_coma+1, i).toInt();
      angle_differences_table[value_index]= val;
      print_info("val: "+(String)val);
      print_info("angle_differences_table["+(String)i+"]: "+(String)angle_differences_table[value_index]);
      value_index++;
      index_of_coma=i;
    }
    if(i+1==angle_differences_table_string.length()){
      int val = angle_differences_table_string.substring(index_of_coma+1).toInt();
      angle_differences_table[value_index]= val;
      print_info("val: "+(String)val);
      print_info("angle_differences_table["+(String)i+"]: "+(String)angle_differences_table[value_index]);
      value_index++;
      index_of_coma=i;
    }
  }

  if(print_info_flag){
    Serial.println("channel_table: ");
    for(int i= 0; i<6;i++){
      Serial.println(channel_table[i]);
    }
    Serial.println("angle_differences_table: ");
    for(int i= 0; i<6;i++){
      Serial.println(angle_differences_table[i]);
    }
  }
  
  turn_on_servo_with_angle_differences(channel_table,angle_differences_table, channel_table_length);
}

String BoolToString(bool b)
{
  return b ? (String)"true" : (String)"false";
}

void set_job_done_property(int dj_id, bool done_value){
  HTTPClient http;
  http.begin(String(ROBOLab_adress)+"device-jobs/"+ String(dj_id)+"/done-flag-value");
  http.addHeader("Content-Type", "application/json-patch+json");
  int httpCode = http.PATCH("{\"done\": "+ BoolToString(done_value) + " }");
  
  Serial.print("[set_job_done_property] http_code: ");
  Serial.println(httpCode);
  http.end();
}

void check_device_job_data(){
  Serial.println("[check_device_job_data]");
  
  HTTPClient http;
  http.begin(String(ROBOLab_adress)+"device-jobs/device/"+ String(robo_arm_device_id)+"/false-done-flag");
  int httpCode = http.GET();
  
  String http_value = http.getString();
  Serial.println("httpCode:");
  Serial.println(httpCode);
  
  if (httpCode > 0)
    {  
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
      else if (error) {
        Serial.print(F("deserializeJson() failed: "));
        Serial.println(error.f_str());
        http.end();
        return;
      }

      dev_job_id = doc["id"];
      const char* job_type = doc["job"]["deviceTypeName"];
      const char* job_name = doc["job"]["name"];
      const char* job_body = doc["body"];

      if((String)job_type=="RoboArm(Arexx RA-1-PRO)"){
          if((String)job_name=="RunAnySequence"){
              
            Serial.println("[animation job: RunAnySequence]");
            print_info("job_body: "+(String)job_body);
            run_any_sequence((String)job_body);
            //delay(2000);

            bool job_done_value = true;
            set_job_done_property(dev_job_id, job_done_value);
          }
          if((String)job_name=="RunAnyDifferenceSequence "){
              
            Serial.println("[animation job: RunAnyDifferenceSequence]");
            print_info("job_body: "+(String)job_body);
            run_any_difference_sequence((String)job_body);
            //delay(2000);

            bool job_done_value = true;
            set_job_done_property(dev_job_id, job_done_value);
          }
      }
    }
  http.end();
}
