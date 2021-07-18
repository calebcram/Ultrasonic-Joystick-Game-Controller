const int SW_pin = A5; 
const int X_pin = A0; 
const int Y_pin = A1; 
const int trigPin = 2;
const int echoPin = 3;
long duration;
int distance;
int x_direction;
int y_direction;
int jump;

void setup() {
  pinMode(SW_pin, INPUT);
  pinMode (trigPin, OUTPUT);
  pinMode (echoPin, INPUT);
  //digitalWrite(SW_pin, HIGH);
  Serial.begin(9600);
}
 
void loop() {
  
  jump = analogRead(SW_pin);
  //Serial.println(jump);
  if (jump >= 400) {
    Serial.write(0); 
    Serial.flush();
    delay(20);
  }

  x_direction = (analogRead(X_pin));
  if (x_direction < 480) {
    Serial.write(1);
    Serial.flush();
    delay(20);
  }
  if (x_direction > 640) 
  {
    Serial.write(2);
    Serial.flush();
    delay(20);
  }
   
  y_direction = (analogRead(Y_pin));
  if (y_direction < 480) {
    Serial.write(3);
    Serial.flush();
    delay(20);
  }
  if (y_direction > 640) 
  {
    Serial.write(4);
    Serial.flush();
    delay(20);
  }

  digitalWrite (trigPin, LOW);
  delayMicroseconds(2);
  digitalWrite(trigPin, HIGH);
  delayMicroseconds(10);
  digitalWrite(trigPin, LOW);

  duration = pulseIn(echoPin, HIGH);
  distance = duration*0.034/2;
  
  Serial.write ("Distance: ");
  Serial.write (distance);
  Serial.flush();
  delay(20);
}
