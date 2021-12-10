# Jumping Taxi

## Inleiding

Via deze tutorial proberen wij u stapsgewijs te gidsen door ons Machine Learning project. We maakten gebruik van Unity, Anaconda en de ML-Agents package om een taxi te kunnen leren springen over politiewagens. Clone dit project en volg onze stappen om tot een gewenst resultaat te komen.
Heel veel plezier met het uitvoeren van deze opdracht!

![image](https://user-images.githubusercontent.com/73060860/145276168-179004ce-8633-4630-8438-68e651c2a230.png)


## Spelverloop

Wanneer u de applicatie start zal u zien dat een taxi centraal in beeld zal staan. Deze taxi is de agent en krijgt de opdracht om over regelmatig aanrijdende politiewagens te springen. De politiewagens noemen we de vijanden of enemies en het is dus de bedoeling dat de taxi deze niet raakt. Boven deze politiewagens zal daarnaast ook een pak friet en een cola te zien zijn die de taxi moet trachten te vangen. Via Machine Learning zullen we deze doelen proberen te bereiken.

## Observaties

De agent of taxi kan 2 situaties observeren:

1. Een naderende politiewagen
2. Geen naderende politiewagen

## Acties

De agent of taxi kan 2 acties uitvoeren om te reageren op de observaties:

1. Springen
2. Niet springen

## Beloning

Als we nu alle mogelijke situaties bekijken die zich kunnen voordoen dan kunnen we volgend beloningssysteem hanteren:

|        Observaties       |      Acties     |  Beloning |
|------------------------- |:---------------:|----------:|
|  Naderende politiewagen  |     Springen    |   +0.1f   |
|  Naderende politiewagen  |  Niet springen  |   -1.0f   |
|          Niets           |     Springen    |   -0.2f   |
|   Soda en Frieten        |     Springen    |   +0.2f  |

Belangrijk om er ook voor te zorgen dat in de functie **OnActionReceived()** er een kleine negatieve score wordt gegeven om te voorkomen dat de agent niet hele tijd zou springen.
```csharp
 public override void OnActionReceived(ActionBuffers actions)
    {
        var vectorAction = actions.DiscreteActions;
        if (vectorAction[0] == 1)
        {
            AddReward(-0.01f);
            Jump();
        }
            

    }
 ```


# Speelruimte
## Taxi
De taxi zal de rol van agent op zich nemen. Hij zal vervolgens dus een obstakel gaan observeren/detecteren zodat er op het juiste moment gesprongen kan worden. 

![image](https://user-images.githubusercontent.com/61239203/145558923-4153a262-916b-4cf9-b0bb-ae724e64d415.png)

Er zal een *Rigidbody &  een Box Collider* moeten worden toegevoegd om collision te kunnen detecteren. 
![image](https://user-images.githubusercontent.com/61239203/145560400-fd122ea5-c5ba-4e5d-8744-6f5531888cc6.png)


Je moet er voor zorgen dat het Taxi.cs script wordt toegevoegd. Zodat je een keyboard knop kan toewijzen moest je de taxi zelf willen besturen. Verder kies je ook de Jump Force waar bij je bepaalt hoe hoog de taxi zal springen. Moest je dit nodig vinden kan je eventueel nog een scoreboard toevoegen om de scores bij te houden. 
![image](https://user-images.githubusercontent.com/61239203/145560667-2809406d-c71d-4198-bef1-3f7839883c27.png)

Tot slot is zijn er de Behavior Paramters. Deze zullen automatisch worden toegevoegd als het taxi scirpt juist is gegeven aan het Taxi object. Wel belangrijk dat je de juist naam gebruikt en de juiste instellingen. Vul de naam Mover in. Als je een eerder getraind brein wilt gebruiken voeg je deze toe bij Model en zet je Behavior Type op Inference Only
![image](https://user-images.githubusercontent.com/61239203/145561536-5412b4b4-c390-4465-86c5-9b7b849d7069.png)

Wil je zelf een training starten plaats je Behavior Type op Default, het is niet nodig om een Model toe te voegen in dit geval.
![image](https://user-images.githubusercontent.com/61239203/145579737-30744af9-e637-4146-977e-ddf0a5b215b2.png)


## Ray perception
De acties die onze taxi uitvoert zijn gebaseerd op de observaties die worden gemaakt. Om dit mogelijk te maken hebben we gebruik gemaakt van de **Ray Perception Sensor3D** component. 

![image](https://user-images.githubusercontent.com/61239203/145287905-6fdc2e5f-cc50-459c-b1e3-1e9579ee26f2.png)

De taxi zal vooral recht voorruit moeten kunnen kijken omdat de obstakels, in dit geval politiewagens van deze richting zullen komen. 
![image](https://user-images.githubusercontent.com/61239203/145288077-c0e823b3-1180-488d-b0f8-76d24f1231f0.png)





## Police car
Het obstakel waar onze agent (de taxi) rekening met zal gaan houden is de politiewagen. Deze is terug te vinden in de Prefab folder. Als de politiewagens worden geïmplementeerd, zal er automatisch een *Rigidbody & Box Collider* toegewezen worden omdat deze in de Prefab al reeds zijn toegevoegd.

![image](https://user-images.githubusercontent.com/61239203/145559650-847df44d-e738-4046-8351-0d563a1b096a.png)


## Beschrijving code
**Spelobjecten scripts (C#)** <br />
Om voor een duidelijke structuur te zorgen maak je best een folder Scripts aan in je Assets van Unity. Hier zullen vervolgens alle scripts gebundeld en bewaard worden.

![image](https://user-images.githubusercontent.com/61239203/145288226-98ffe34b-dfa1-45d8-a918-217d0f72324a.png)

**Environment.cs** <br />
In het Environment.cs script staat de code die ervoor zorgt dat de Enemies (PoliceCar) tevoorschijn komen op de weg. Bij het starten van het Unity project zal dit dus automatisch gebeuren. <br />
 <br />
In onze Environment klasse zal de functie SpawnEnemies er voor zorgen dat de enemies 1 voor 1 tevoorschijn komen, dit binnen een zelf gekozen tijdspanne.

```csharp
 public void SpawnEnemies()
    {
        GameObject newEnemy = Instantiate(policeCarPrefab.gameObject);

        newEnemy.transform.SetParent(enemies.transform);
        newEnemy.transform.localPosition = enemies.transform.localPosition;
        newEnemy.transform.localRotation = enemies.transform.localRotation;

    }
```

**Obstacle.cs** <br/>
In dit script wordt de snelheid bepaald van de obstakels (politiewagens). Er zal een min en max speed worden toegewezen volgens 2 SerializeFields.
```csharp
[SerializeField] private float minSpeed;
[SerializeField] private float maxSpeed;
```
![image](https://user-images.githubusercontent.com/61239203/145625865-119d444e-898d-4523-8123-236f73c5c467.png)

We hebben er voor gekozen om een random snelheid te genereren om er voor te zorgen dat het voor onze Agent niet te makkelijk is.

```csharp
private Rigidbody Rigidbody;
private void Awake()
{
    Rigidbody = GetComponent<Rigidbody>();
}
private void FixedUpdate()
{
    Rigidbody.velocity = Vector3.back * Random.Range(minSpeed,maxSpeed);
}
```

**Taxi.cs** <br />
Het Taxi script wordt gebruikt door onze Agent. <br> ![image](https://user-images.githubusercontent.com/61239203/145628286-507ec4b0-9454-46fe-80de-5fc0dd4832ff.png)
 <br />
 
 **Variabelen** <br/>
Er worden 2 SerializeFields toegevoegd die achteraf nog worden opgevuld in het Taxi Object van Unity zoals eerder werd besproken. 
```csharp
    [SerializeField] private float jumpForce;
    [SerializeField] private KeyCode jumpKey;
    private Vector3 startingPosition;
    private bool canJump = true;
    private bool gotFries = false;
    private Rigidbody body;
    private Environment environment;
    public event Action OnReset;
```

De canJump variabele is een boolean die default op True staat. Dit zal zo blijven wanneer de taxi niet in de lucht is. De boolean zal de waarde False krijgen als de taxi zich in de lucht bevindt. De gotFries boolean is noodzakelijk om een beloning te geven aan de taxi als hij succesvol over het obstakel (Politie wagen) is gesprongen.


**OnEpisodeBegin**<br/>
Bij het begin van elke episode, zal de canJump boolean op true gezet worden en de Agent wordt op zijn startpositie geplaatst. De environment wordt opnieuw gereset.
```csharp
 public override void OnEpisodeBegin()
    {
        canJump = true;
        transform.position = startingPosition;
        body.velocity = Vector3.zero;

        environment.ClearEnvironment();
    }
 ```
 **Heuristic**<br/>
 De Heuristic functie is vooral van toepassing bij het zelf besturen van de Agent. De methode zal helpen om de correcte werking en beloning van de Taxi te testen.
 ```csharp
   public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut[0] = 0;

        if (Input.GetKey(jumpKey))
        {
            discreteActionsOut[0] = 1;
        }
           
    }
  ```
  
**OnActionReceived**<br/>
Deze methode wordt geïmplementeerd om het gedrag van de agent bij elke stap te specificeren, op basis van de opgegeven actie. 
<br/>
Deze methode zal er voor zorgen dat de Agent (Taxi) zal gaan springen als de waarde van canJump True is. Op deze manier zal de Agent alleen kunnen springen als hij zich op de Road bevindt. De waarde 1 staat voor de actie springen. De waarde 0 staat voor het stilstaan v.d. Agent. 
```csharp
  public override void OnActionReceived(ActionBuffers actions)
    {
        var vectorAction = actions.DiscreteActions;
        if (vectorAction[0] == 1)
        {
            AddReward(-0.01f);
            Jump();
        }
            

    }
```

```csharp
  private void Jump()
    {
        if (canJump)
        {
            body.AddForce(new Vector3(0, jumpForce, 0), ForceMode.VelocityChange);
            canJump = false;
        }


    }
```


 **OnCollisionEnter**<br/>
 
De methode OnCollisionEnter wordt aangeroepen wanneer een collider/rigidbody een ander collider/rigidbody aanraakt. In ons project wordt er eerst en vooral gekeken of er een collision is met de Road. De CompareTag() methode wordt gebruikt om de identiteit van het object te achterhalen waarmee de Agent (Taxi) in botsing komt.

```csharp
  private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Road") )
        {
            canJump = true;
        }
    }
 ```
 
 <br>
In dit if-statement wordt er gekeken of er zich een collision voordoet met de PoliCar (Obstacle). Moest deze situatie zich voordoen, dan zal er een negatieve beloning van -1f worden toegewezen. Tot slot wordt de huidige episode beëndigd. 

 ```csharp
        if (collision.transform.CompareTag("PoliceCar"))
        {
            Destroy(collision.gameObject);
            AddReward(-1f);
            EndEpisode();
        }
```

Rijd de Agent (Taxi) op de Road en heeft de frietjes kunnen opnemen boven de PoliceCar, dan wordt canJump op True geplaatst zodat er terug gesprongen kan worden. De gotFries boolean zal terug op False getet worden. 

```csharp
         if (collision.gameObject.CompareTag("Road") && gotFries)
        {
            canJump = true;
            gotFries = false;
        }
    }
```

<br>
Als laatste geven we de Agent (Taxi) een beloning als er succesvol over het obstakel (PoliceCar) is gesprongen. Wanneer er collision is met de tag "Point" , zal de Agent een reward van +0.2f ontvangen. De boolean gotFries krijgt terug de waarde True.
Dit alles wordt geïmplementeerd door onderstaande methode:

```csharp
private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Point"))
        {
            AddReward(0.2f);
            gotFries = true;
        }
    }
```

<br>
De point tag zie je hier duidelijk weergeven. Het is belangrijk dat je deze ook zelf toevoegt aan de Prefab French Frice. <br>

![image](https://user-images.githubusercontent.com/61239203/145637484-081b4d84-7735-4659-8879-3f7b39393348.png)

## Training
Als laatste stap bekijken we kort de trainingsfase. Maak een folder Learning aan in de Assets. In deze folder maak je een .yml file aan. In ons voorbeeld is dit dus Mover.yml. In dit bestand voeg je de juiste parameters toe. Tijdens onze training hebben we onderstaande waardes gebruikt: 

```
behaviors:
  Mover:
    trainer_type: ppo
    hyperparameters:
      batch_size: 32
      buffer_size: 256
      learning_rate: 0.0003
      beta: 0.005
      epsilon: 0.2
      lambd: 0.95
      num_epoch: 3
      learning_rate_schedule: linear
    network_settings:
      normalize: false
      hidden_units: 20
      num_layers: 1
      vis_encode_type: simple
    reward_signals:
      extrinsic:
        gamma: 0.9
        strength: 1.0
    keep_checkpoints: 5
    max_steps: 500000
    time_horizon: 3
    summary_freq: 2000
    threaded: true

```

**Belangrijk** dat je er voor zorgt dat de Behavior Name in het Taxi object overeenkomt met die in het .yml bestand: 
![image](https://user-images.githubusercontent.com/61239203/145582496-8db57201-6e2c-4caf-850f-7c093143198b.png)

## Tensorboard Resultaten
De resultaten van de training zijn zichtbaar in onderstaande grafieken: 
![image](https://user-images.githubusercontent.com/61239203/145583377-881abc37-5bcb-4bd4-b45d-b363c813b0f7.png) <br>
Je merkt op dat in de grafiek van de Cumulative Reward een duidelijke stijging aanwezig is. Het lijkt er dus op dat de agent steeds beter presteert en vaker springt zonder op de politiewagen te botsen. Dit kun je dus ook waarnemen in de Episode Length, waar je kan zien dat lengte van de episode langer is naarmate het einde van de training. 








