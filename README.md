# Bird in Cave

[English version](#english-version)

**Bird in Cave** — 2D аркадная игра, созданная по мотивам [Don't Touch the Spikes](https://play.google.com/store/search?q=don%27t%20touch%20the%20spikes&c=apps).

<img width="223" height="299" alt="image" src="https://github.com/user-attachments/assets/341a236f-a1a7-4373-989f-75f97f79669d" />

Основная задача игрока — управлять птицей, которая летает между двумя стенами, избегая появляющихся на них шипов. Цель игры — продержаться как можно дольше и набрать максимальное количество очков.

Проект разработан на Unity с использованием **Entity Component System (ECS)** совместно с классическим подходом на основе MonoBehaviour.

## Игровой процесс

- Управление птицей с помощью прыжков.
- Смена направления движения при столкновении со стеной.
- Генерация шипов на стенах с использованием ECS.
- Подсчёт очков за каждую смену направления движения.
- Завершение игры при столкновении с шипами.

## Техническая реализация

Одной из основных особенностей проекта является совместное использование ECS и MonoBehaviour. Разные части игры реализованы с помощью наиболее подходящего для них подхода.

### Entity Component System (ECS)

ECS используется для генерации и управления шипами.

**EnemySpawnConfigAuthoring**
- Настройка параметров генерации шипов через Inspector.
- Определение двух точек, между которыми размещаются шипы.
- Настройка количества доступных позиций и количества создаваемых шипов.
- Преобразование настроек Authoring в ECS Entity с помощью Baker.

**EnemySystem**
- Создание и удаление Entity с шипами.
- Выбор стороны генерации в зависимости от направления движения игрока.
- Случайное распределение шипов между доступными позициями.
- Использование EntityCommandBuffer для отложенного выполнения структурных изменений ECS.

Для определения стороны генерации используются специальные компоненты-теги: `LeftWallTag`, `RightWallTag`, `LeftWallEnemyTag` и `RightWallEnemyTag`.

### MonoBehaviour

Игрок и его основные игровые механики реализованы с использованием стандартных компонентов Unity и MonoBehaviour.

Основные скрипты:

| Скрипт | Описание |
|---|---|
| `Movement` | Управление движением птицы, прыжками и изменением направления |
| `AnimationChange` | Управление анимацией и поворотом персонажа |
| `AttachmentToSpike` | Обработка столкновений с шипами |
| `ScoreChange` | Подсчёт игровых очков |
| `DirectionSignalChangeForECS` | Передача информации о смене направления движения в ECS |

Для физики и взаимодействия с игровым окружением используются `Rigidbody2D`, `BoxCollider2D` и `SpriteRenderer`.

### Взаимодействие ECS и MonoBehaviour

Для организации взаимодействия между ECS и классическими GameObject используются дополнительные компоненты и классы-посредники.

**Передача сигнала о смене направления**

- `DirectionChangeConfigAuthoring` создаёт ECS-компонент `DirectionChangeSignal`.
- `DirectionSignalChangeForECS` получает доступ к EntityManager и изменяет значение сигнала при столкновении игрока со стеной.
- `EnemySystem` считывает сигнал и определяет сторону, на которой необходимо создать новую группу шипов.

**Создание физических коллайдеров**

- `ECSProxyAuthoring` передаёт в ECS конфигурацию с префабом коллайдера.
- `EcsProxyConfig` хранит ссылки на созданные GameObject и позволяет управлять их жизненным циклом.
- При генерации шипов ECS создаёт соответствующие GameObject-коллайдеры, с которыми взаимодействует физика игрока.

Таким образом, ECS отвечает за генерацию игровых объектов, а MonoBehaviour обеспечивает управление игроком и взаимодействие с физикой Unity.

## Используемые технологии

- Unity
- C#
- Unity Entities (ECS)
- MonoBehaviour
- Unity Physics 2D
- TextMeshPro
- EntityCommandBuffer

## Цель проекта

Основной целью проекта было изучение Unity ECS и практическое применение Entity Component System в 2D-игре, а также получение опыта совместного использования ECS и классического подхода разработки на MonoBehaviour.

---

# English version

**Bird in Cave** is a 2D arcade game inspired by [Don't Touch the Spikes](https://play.google.com/store/search?q=don%27t%20touch%20the%20spikes&c=apps).

<img width="223" height="299" alt="image" src="https://github.com/user-attachments/assets/341a236f-a1a7-4373-989f-75f97f79669d" />

The main objective is to control a bird flying between two walls while avoiding spikes that appear on them. The goal is to survive as long as possible and achieve the highest score.

The project was developed in Unity using **Entity Component System (ECS)** alongside the traditional MonoBehaviour approach.

## Gameplay

- Control the bird using jumps.
- Change movement direction when colliding with a wall.
- Spawn spikes on the walls using ECS.
- Earn points each time the bird changes direction.
- End the game when colliding with spikes.

## Technical Implementation

One of the main features of the project is the combination of ECS and MonoBehaviour. Different parts of the game are implemented using the approach best suited for their functionality.

### Entity Component System (ECS)

ECS is used to spawn and manage spikes.

**EnemySpawnConfigAuthoring**
- Configure spike spawning parameters through the Inspector.
- Define two positions between which spikes can be placed.
- Set the number of available spawn positions and the number of spikes to spawn.
- Convert Authoring settings into ECS Entities using a Baker.

**EnemySystem**
- Instantiate and destroy spike Entities.
- Select the spawning side based on the player's movement direction.
- Randomly distribute spikes among the available positions.
- Use EntityCommandBuffer to record and execute structural changes to the ECS World.

Special tag components are used to identify the spawning side: `LeftWallTag`, `RightWallTag`, `LeftWallEnemyTag` and `RightWallEnemyTag`.

### MonoBehaviour

The player and its main gameplay mechanics are implemented using standard Unity components and MonoBehaviour scripts.

Main scripts:

| Script | Description |
|---|---|
| `Movement` | Handles bird movement, jumping and direction changes |
| `AnimationChange` | Controls character animations and sprite rotation |
| `AttachmentToSpike` | Handles collisions with spikes |
| `ScoreChange` | Tracks the player's score |
| `DirectionSignalChangeForECS` | Sends information about movement direction changes to ECS |

`Rigidbody2D`, `BoxCollider2D` and `SpriteRenderer` are used for physics and interaction with the game environment.

### ECS and MonoBehaviour Interaction

Additional components and intermediary classes are used to connect ECS with traditional Unity GameObjects.

**Direction Change Signal**

- `DirectionChangeConfigAuthoring` creates the ECS component `DirectionChangeSignal`.
- `DirectionSignalChangeForECS` accesses the EntityManager and updates the signal when the player collides with a wall.
- `EnemySystem` reads the signal and determines which side should receive a new group of spikes.

**Physics Collider Creation**

- `ECSProxyAuthoring` provides ECS with a configuration containing the collider prefab.
- `EcsProxyConfig` stores references to the created GameObjects and allows their lifecycle to be managed.
- When spawning spikes, ECS also instantiates corresponding GameObject colliders, which interact with the player's physics.

This approach allows ECS to handle game object spawning, while MonoBehaviour is responsible for player control and interaction with Unity's physics system.

## Technologies

- Unity
- C#
- Unity Entities (ECS)
- MonoBehaviour
- Unity Physics 2D
- TextMeshPro
- EntityCommandBuffer

## Project Goal

The main goal of this project was to learn Unity ECS and gain practical experience applying Entity Component System in a 2D game, as well as combining ECS with the traditional MonoBehaviour development approach.
